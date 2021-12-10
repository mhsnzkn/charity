using AutoMapper;
using Business.Abstract;
using Business.Utility;
using Data.Constants;
using Data.Entities;
using Data.Models;
using DataAccess.Abstract;
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
                volunteerDal.Add(entity);
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
