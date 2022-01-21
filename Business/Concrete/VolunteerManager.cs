using AutoMapper;
using Business.Abstract;
using Data.Utility.Results;
using Data.Constants;
using Data.Entities;
using Data.Models;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Data.Dtos.Datatable;
using System.Text.Json;

namespace Business.Concrete
{
    public class VolunteerManager : IVolunteerManager
    {
        private readonly IVolunteerDal volunteerDal;
        private readonly IMapper mapper;

        public VolunteerManager(IVolunteerDal VolunteerDal, IMapper mapper)
        {
            this.volunteerDal = VolunteerDal;
            this.mapper = mapper;
        }

        public async Task<Result> Add(VolunteerModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Volunteer>(model);

                entity.Status = Enums.VolunteerStatus.Trial;
                entity.CrtDate = DateTime.Now;
                volunteerDal.Add(entity);
                await volunteerDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }
            
            return result;
        }

        public async Task<Result> Delete(Volunteer entity)
        {
            var result = new Result();
            try
            {
                volunteerDal.Delete(entity);
                await volunteerDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<VolunteerDto> GetByIdAsync(int id)
        {
            return mapper.Map<VolunteerDto>(await volunteerDal.GetByIdAsync(id));
        }

        public async Task<TableResponseDto<VolunteerListDto>> GetTable(VolunteerTableParamsDto param)
        {
            var query = volunteerDal.Get().OrderByDescending(a=>a.CrtDate).AsQueryable();
            if(param.Status != Enums.VolunteerStatus.All)
                query = query.Where(a=>a.Status == param.Status);

            if (!string.IsNullOrEmpty(param.SearchString))
                query = query.Where(a => a.FirstName.Contains(param.SearchString) ||
                                        a.LastName.Contains(param.SearchString) ||
                                        a.Email.Contains(param.SearchString) ||
                                        a.Address.Contains(param.SearchString) ||
                                        a.PostCode.Contains(param.SearchString) ||
                                        a.HomeNumber.Contains(param.SearchString) ||
                                        a.MobileNumber.Contains(param.SearchString));

            var total = await query.CountAsync();
            if(param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var tableModel = new TableResponseDto<VolunteerListDto>()
            {
                Records = await mapper.ProjectTo<VolunteerListDto>(query).ToListAsync(),
                TotalItems = total,
                PageIndex = (param.Start/param.Length)+1
            };

            return tableModel;
        }

        public async Task<Result> Update(VolunteerModel model)
        {
            var result = new Result();
            try
            {
                var entity = await volunteerDal.GetByIdAsync(model.Id);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Address = model.Address;
                entity.PostCode = model.PostCode;
                entity.MobileNumber = model.MobileNumber;
                entity.HomeNumber = model.HomeNumber;
                entity.Reason = model.Reason;
                entity.Organisations = model.Organisations;
                entity.Skills = model.Skills;

                entity.UptDate = DateTime.Now;

                await volunteerDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
        }

        public async Task<Result> Approve(int id)
        {
            var result = new Result();
            var volunteer = await volunteerDal.GetByIdAsync(id);
            if(volunteer == null)
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            if(volunteer.Status == Enums.VolunteerStatus.Cancelled)
            {
                result.SetError(UserMessages.VolunteerRejected);
                return result;
            }
            if(volunteer.Status == Enums.VolunteerStatus.Completed)
            {
                result.SetError(UserMessages.VolunteerCompleted);
                return result;
            }
            
            volunteer.Status = volunteer.Status + 1;
            await volunteerDal.Save();
            return result;
        }
        public async Task<Result> Cancel(int id, string cancellationReason)
        {
            var result = new Result();
            var volunteer = await volunteerDal.GetByIdAsync(id);
            if(volunteer == null)
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            if(volunteer.Status == Enums.VolunteerStatus.Cancelled)
            {
                result.SetError(UserMessages.VolunteerRejected);
                return result;
            }

            volunteer.Status = Enums.VolunteerStatus.Cancelled;
            volunteer.CancellationReason = cancellationReason;
            await volunteerDal.Save();
            return result;
        }
    }
}
