using AutoMapper;
using Business.Abstract;
using Business.Utility;
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

                entity.CrtDate = DateTime.Now;
                volunteerDal.Add(entity);
                await volunteerDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, UserMessages.Fail);
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
                result.SetError(ex.Message, UserMessages.Fail);
            }

            return result;
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
                result.SetError(ex.Message, UserMessages.Fail);
            }
            return result;
        }
    }
}
