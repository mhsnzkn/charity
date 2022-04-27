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
using Business.Utility.MailService;

namespace Business.Concrete
{
    public class VolunteerManager : IVolunteerManager
    {
        private readonly IVolunteerDal volunteerDal;
        private readonly IMapper mapper;
        private readonly IMailService mailService;
        private readonly ICommonFileManager commonFileManager;

        public VolunteerManager(IVolunteerDal VolunteerDal, IMapper mapper, 
            IMailService mailService, ICommonFileManager commonFileManager)
        {
            this.volunteerDal = VolunteerDal;
            this.mapper = mapper;
            this.mailService = mailService;
            this.commonFileManager = commonFileManager;
        }

        public async Task<Result> Add(VolunteerModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Volunteer>(model);

                entity.Status = VolunteerStatus.ApplicationPending;
                volunteerDal.Add(entity);
                await volunteerDal.Save();

            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }
            
            return result;
        }

        public async Task<Result> AddWithMail(VolunteerModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Volunteer>(model);

                entity.Status = VolunteerStatus.ApplicationPending;
                volunteerDal.Add(entity);
                await volunteerDal.Save();

                var emailResult = await mailService.SendNewApplicationMail(model.FirstName, model.LastName, model.Email);
                if (emailResult.Error)
                {
                    result.Message += UserMessages.EmailSendFailed;
                }
            }
            catch (Exception ex)
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
                var entity = await volunteerDal.GetByIdAsync(id);
                if (entity == null) 
                {
                    result.SetError(UserMessages.DataNotFound);
                    return result;
                }
                volunteerDal.Delete(entity);
                await volunteerDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
        public async Task<VolunteerDetailDto> GetDetailDto(int id)
        {
            var query = volunteerDal.Get(a=>a.Id == id);
            return await mapper.ProjectTo<VolunteerDetailDto>(query).FirstOrDefaultAsync();
        }

        public async Task<TableResponseDto<VolunteerTableDto>> GetTable(VolunteerTableParamsDto param)
        {
            var query = volunteerDal.Get();
            if(!string.IsNullOrEmpty(param.Status))
            {
                var status = Enum.Parse<VolunteerStatus>(param.Status);
                query = query.Where(a=>a.Status == status);
            }

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

            var tableModel = new TableResponseDto<VolunteerTableDto>()
            {
                Records = await mapper.ProjectTo<VolunteerTableDto>(query).ToListAsync(),
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

        public void Update(Volunteer entity) => volunteerDal.Update(entity);
        public void SetStatus(Volunteer volunteer, VolunteerStatus status)
        {
            volunteer.Status = status;
            volunteerDal.Update(volunteer);
        }

        public async Task<ResultData<Volunteer>> Approve(Volunteer volunteer)
        {
            var result = new ResultData<Volunteer>();
            if(volunteer == null)
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            if(volunteer.Status == VolunteerStatus.Cancelled)
            {
                result.SetError(UserMessages.VolunteerRejected);
                return result;
            }
            if(volunteer.Status == VolunteerStatus.Completed)
            {
                result.SetError(UserMessages.VolunteerCompleted);
                return result;
            }
            
            volunteer.Status = volunteer.Status + 1;
            await volunteerDal.Save();

            // Document deletion
            if (volunteer.Status > VolunteerStatus.DBSDocument)
                await commonFileManager.DeleteVolunteerFile(volunteer.Id);

            return result;
        }

        public async Task<Result> SendStatusMail(Volunteer volunteer)
        {
            Result result = null;
            switch (volunteer.Status)
            {
                case VolunteerStatus.DBS:
                    result = await mailService.SendDBSMail(volunteer.FirstName, volunteer.LastName, volunteer.Email);
                    break;
                case VolunteerStatus.DBSDocument:
                    result = await mailService.SendDBSUploadDocMail(volunteer.FirstName, volunteer.LastName, volunteer.Email, volunteer.Key);
                    break;
                case VolunteerStatus.Agreement:
                    result = await mailService.SendAgreementMail(volunteer.FirstName, volunteer.LastName, volunteer.Email, volunteer.Key);
                    break;
                case VolunteerStatus.Induction:
                    break;
                case VolunteerStatus.Completed:
                    result = await mailService.SendCompletedMail(volunteer.FirstName, volunteer.LastName, volunteer.Email);
                    break;
                default:
                    break;
            }

            return result;
        }
        public async Task<Result> Cancel(int id, string cancellationReason)
        {
            var result = new Result();
            var volunteer = await volunteerDal.GetByIdAsync(id);

            if(volunteer == null)
                return result.SetError(UserMessages.DataNotFound);
            
            if(volunteer.Status == VolunteerStatus.Cancelled)
                return result.SetError(UserMessages.VolunteerRejected);

            result = await volunteerDal.Cancel(volunteer, cancellationReason);
            await commonFileManager.DeleteVolunteerFile(id);

            return result;
        }

        public async Task<Result> OnHold(int id)
        {
            var result = new Result();
            try
            {
                var volunteer = await volunteerDal.GetByIdAsync(id);

                if (volunteer == null)
                    return result.SetError(UserMessages.DataNotFound);

                if (volunteer.Status == VolunteerStatus.Cancelled)
                    return result.SetError(UserMessages.VolunteerRejected);

                SetStatus(volunteer, VolunteerStatus.OnHold);
                await volunteerDal.Save();
                
                var mailResult = await mailService.SendOnHoldMail(volunteer.FirstName, volunteer.LastName, volunteer.Email);
                if (mailResult.Error)
                    result.AddMessage(UserMessages.EmailSendFailed);
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
        public async Task<Result> ApproveAndCheckMail(int id)
        {
            var volunteer = await volunteerDal.GetByIdAsync(id);
            var result = await Approve(volunteer);
            if (result.Error)
                return result.SetError(UserMessages.Fail);

            var emailResult = await SendStatusMail(volunteer);
            if (emailResult is not null && emailResult.Error)
            {
                result.AddMessage(emailResult.Message);
            }
            return result;
        }

        public async Task<Result> UploadDocuments(VolunteerDocumentPostModel model)
        {
            var result = new Result();
            var volunteer = await volunteerDal.GetByKey(model.Key);
            if (volunteer == null)
                return result.SetError(UserMessages.UserNotFound);

            if(volunteer.Status != VolunteerStatus.DBSDocument)
                return result.SetError(UserMessages.FileCannotBeUploaded);

            if (model.Files is null)
                return result.SetError(UserMessages.Fail);

            for (int i = 0; i < model.Files.Length; i++)
            {
                var fileResult = await commonFileManager.UploadVolunteerFile(volunteer, model.Files[i], volunteer.Id + "-" + i, CommonFileTypes.DbsDocument);
                if (fileResult.Error)
                {
                    result.SetError(fileResult.Message);
                    break;
                }
            }

            return result;
        }

        public async Task<Volunteer> GetVolunteerByKey(Guid key)
        {
            return await volunteerDal.GetByKey(key);
        }
    }
}
