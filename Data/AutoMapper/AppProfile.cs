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
                .ForMember(o => o.Files, m => m.MapFrom(o => o.VolunteerFiles.Select(x => x.CommonFile).ToList()))
                .ForMember(o => o.Agreements , m => m.MapFrom(o => o.VolunteerAgreements.Select(x =>new AgreementTableDto
                {
                    Id = x.Agreement.Id,
                    IsActive = x.Agreement.IsActive,
                    Date = x.UptDate ?? x.CrtDate,
                    Title = x.Agreement.Title,
                }).ToList()));
            #endregion

            #region User
            CreateMap<User, UserAccountInfoDto>();
            CreateMap<User, UserListDto>();
            CreateMap<UserEditModel, User>().ReverseMap();
            #endregion

            #region Agreement
            CreateMap<Agreement, AgreementModel>().ReverseMap();
            CreateMap<Agreement, AgreementTableDto>()
                .ForMember(o => o.Date, m => m.MapFrom(o => o.UptDate ?? o.CrtDate));
            #endregion

            #region Expense
            CreateMap<Expense, ExpenseTableDto>()
                .ForMember(o => o.UserName, m => m.MapFrom(o => o.Volunteer.FirstName +" "+o.Volunteer.LastName));
            CreateMap<Expense, ExpenseModel>()
                .ForMember(o => o.CommonFilePath, m => m.MapFrom(o => o.CommmonFile.Path));
            #endregion
        }
    }
}
