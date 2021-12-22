using AutoMapper;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using Newtonsoft.Json;
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
                .ForMember(o => o.Name, m => m.MapFrom(o => o.FirstName + " " + o.LastName))
                .ForMember(o => o.StatusName, m => m.MapFrom(o => o.Status.ToString()));
            CreateMap<Volunteer, VolunteerDto>()
                .ForMember(a => a.Organisations, o => o.MapFrom(a => JsonConvert.DeserializeObject(a.Organisations)))
                .ForMember(a => a.Skills, o => o.MapFrom(a => JsonConvert.DeserializeObject(a.Skills)));
            #endregion

            #region User
            CreateMap<User, UserAccountInfoDto>();
            CreateMap<User, UserListDto>()
                .ForMember(a => a.StatusName, m => m.MapFrom(a => a.Status.ToString()));
            CreateMap<UserEditModel, User>().ReverseMap();
            #endregion
        }
    }
}
