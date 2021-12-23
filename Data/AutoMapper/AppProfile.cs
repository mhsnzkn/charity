using AutoMapper;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Extensions;

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
            CreateMap<Volunteer, VolunteerDto>()
                .ForMember(a => a.Organisations, o => o.MapFrom(mapExpression: a => JsonExtension.GetJsonObject<List<Organisations>>(a.Organisations)))
                .ForMember(a => a.Skills, o => o.MapFrom(a => JsonExtension.GetJsonObject<List<Skills>>(a.Skills)));
            #endregion

            #region User
            CreateMap<User, UserAccountInfoDto>();
            CreateMap<User, UserListDto>();
            CreateMap<UserEditModel, User>().ReverseMap();
            #endregion
        }
    }
}
