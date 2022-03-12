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
            CreateMap<Volunteer, VolunteerTableDto>()
                .ForMember(o => o.Name, m => m.MapFrom(o => o.FirstName + " " + o.LastName));
            CreateMap<Volunteer, VolunteerDto>();
            CreateMap<Volunteer, VolunteerDetailDto>()
                .ForMember(o => o.Files, m => m.MapFrom(o => o.VolunteerFiles.Select(x => x.CommonFile).ToList()));
            #endregion

            #region User
            CreateMap<User, UserAccountInfoDto>();
            CreateMap<User, UserListDto>();
            CreateMap<UserEditModel, User>().ReverseMap();
            #endregion

            #region Agreement
            CreateMap<Agreement, AgreementModel>().ReverseMap();
            #endregion
        }
    }
}
