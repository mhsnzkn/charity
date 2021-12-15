using AutoMapper;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.AutoMapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            #region Volunteer
            CreateMap<VolunteerModel, Volunteer>()
                .ForMember(a => a.PostCode, o => o.MapFrom(a => a.PostCode.ToUpper()));
            CreateMap<Volunteer, VolunteerListDto>()
                .ForMember(o => o.Name, m => m.MapFrom(o => o.FirstName + " " + o.LastName));
            #endregion
        }
    }
}
