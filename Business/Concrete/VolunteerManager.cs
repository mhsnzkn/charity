﻿using AutoMapper;
using Business.Abstract;
using Data.Utility.Results;
using Data.Constants;
using Data.Entities;
using Data.Models;
using DataAccess.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
                entity.Organisations = JsonConvert.SerializeObject(model.Organisations);
                entity.Skills = JsonConvert.SerializeObject(model.Skills);

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

        public async Task<VolunteerTableDto> GetTable(Expression<Func<Volunteer, bool>> expression = null)
        {
            var tableModel = new VolunteerTableDto()
            {
                Records = await mapper.ProjectTo<VolunteerListDto>(volunteerDal.Get()).ToListAsync(),
                Total = await volunteerDal.Get().CountAsync(),
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
                entity.Organisations = JsonConvert.SerializeObject(model.Organisations);
                entity.Skills = JsonConvert.SerializeObject(model.Skills);

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
                result.SetError(UserMessages.VolunteerNotFound);
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
                result.SetError(UserMessages.VolunteerNotFound);
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
