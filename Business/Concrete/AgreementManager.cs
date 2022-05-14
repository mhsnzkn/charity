using AutoMapper;
using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AgreementManager : IAgreementManager
    {
        private readonly IAgreementDal agreementDal;
        private readonly IMapper mapper;
        private readonly IVolunteerAgreementDal volunteerAgreementDal;
        private readonly IVolunteerManager volunteerManager;

        public AgreementManager(IAgreementDal agreementDal, IMapper mapper, IVolunteerAgreementDal volunteerAgreementDal, IVolunteerManager volunteerManager)
        {
            this.agreementDal = agreementDal;
            this.mapper = mapper;
            this.volunteerAgreementDal = volunteerAgreementDal;
            this.volunteerManager = volunteerManager;
        }

        public async Task<TableResponseDto<AgreementTableDto>> GetTable(TableParams param)
        {
            var query = agreementDal.Get().OrderBy(a=>a.Order).AsQueryable();

            if (!string.IsNullOrEmpty(param.SearchString))
                query = query.Where(a => a.Title.Contains(param.SearchString));

            var total = await query.CountAsync();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var tableModel = new TableResponseDto<AgreementTableDto>()
            {
                Records = await mapper.ProjectTo<AgreementTableDto>(query).ToListAsync(),
                TotalItems = total,
                PageIndex = (param.Start / param.Length) + 1
            };

            return tableModel;
        }

        public async Task<Agreement> GetById(int id)
        {
            return await agreementDal.GetByIdAsync(id);
        }

        public async Task<AgreementModel> GetModelById(int id)
        {
            return await agreementDal.GetModelById(id);
        }

        public async Task<List<Agreement>> GetActiveAgreements()
        {
            return await agreementDal.Get(a=>a.IsActive).ToListAsync();
        }

        public async Task<Result> Add(AgreementModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Agreement>(model);
                agreementDal.Add(entity);
                await agreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Update(AgreementModel model)
        {
            var result = new Result();
            try
            {
                var entity = await agreementDal.GetByIdAsync(model.Id);
                var existingData = await volunteerAgreementDal.Get(a => a.AgreementId == model.Id).AnyAsync();
                if (existingData)
                {
                    if(entity.Title != model.Title || entity.Content != model.Content || entity.Order != model.Order)
                    {
                        result.SetError(UserMessages.AgreementInUse);
                    }
                    if (entity.IsActive != model.IsActive)
                    {
                        entity.IsActive = model.IsActive;
                        result.AddMessage(UserMessages.AgreementDisabled);
                    }
                    await agreementDal.Save();
                    return result;
                }

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.Order = model.Order;
                entity.IsActive = model.IsActive;
                agreementDal.Update(entity);
                await agreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> SaveVolunteerAgreements(VolunteerAgreementPostModel model)
        {
            var result = new Result();
            if (model.AgreementIds == null || model.AgreementIds.Length == 0)
                return result.SetError(UserMessages.DataNotFound);

            var volunteer = await volunteerManager.GetVolunteerByKey(model.Key);
            if (volunteer is null)
                return result.SetError(UserMessages.UserNotFound);

            try
            {
                var existingAgreements = await volunteerAgreementDal.Get(a => a.VolunteerId == volunteer.Id).ToListAsync();
                foreach (var item in model.AgreementIds)
                {
                    if (existingAgreements.Select(a => a.AgreementId).Contains(item))
                        continue;
                    var newAgreement = new VolunteerAgreement
                    {
                        AgreementId = item,
                        Volunteer = volunteer
                    };
                    volunteerAgreementDal.Add(newAgreement);
                }
                if (volunteer.Status == VolunteerStatus.Agreement)
                {
                    volunteerManager.SetStatus(volunteer, VolunteerStatus.Induction);
                }
                await volunteerAgreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            try
            {
                var entity = await agreementDal.GetByIdAsync(id);
                var existingData = await volunteerAgreementDal.Get(a => a.AgreementId == id).AnyAsync();
                if (existingData)
                {
                    entity.IsActive = false;
                    await agreementDal.Save();
                    return result.SetMessage(UserMessages.AgreementDisabled);
                }

                agreementDal.Delete(entity);
                await agreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
    }
}
