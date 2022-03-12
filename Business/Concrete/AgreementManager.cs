﻿using AutoMapper;
using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AgreementManager : IAgreementManager
    {
        private readonly IAgreementDal agreementDal;
        private readonly IMapper mapper;
        private readonly IVolunteerAgreementDal volunteerAgreementDal;

        public AgreementManager(IAgreementDal agreementDal, IMapper mapper, IVolunteerAgreementDal volunteerAgreementDal)
        {
            this.agreementDal = agreementDal;
            this.mapper = mapper;
            this.volunteerAgreementDal = volunteerAgreementDal;
        }

        public async Task<TableResponseDto<AgreementTableDto>> GetTable(TableParams param)
        {
            var query = agreementDal.Get().OrderByDescending(a => a.Order).AsQueryable();

            if (!string.IsNullOrEmpty(param.SearchString))
                query = query.Where(a => a.Title.Contains(param.SearchString));

            var total = await query.CountAsync();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var tableModel = new TableResponseDto<AgreementTableDto>()
            {
                Records = await mapper.ProjectTo<AgreementTableDto>(query).ToListAsync(),
                TotalItems = total,
                PageIndex = (param.Start / param.Length) + 1
            };

            return tableModel;
        }

        public async Task<Agreement> GetById(int id)
        {
            return await agreementDal.GetByIdAsync(id);
        }

        public async Task<Result> Add(AgreementModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Agreement>(model);
                agreementDal.Add(entity);
                await agreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Update(AgreementModel model)
        {
            var result = new Result();
            try
            {
                var entity = await agreementDal.GetByIdAsync(model.Id);
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.Order = model.Order;
                agreementDal.Update(entity);
                await agreementDal.Save();
            }
            catch (Exception)
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
                var existingData = await volunteerAgreementDal.Get(a => a.AgreementId == id).AnyAsync();
                if (existingData)
                    return result.SetError(UserMessages.AgreementInUse);

                var entity = new Agreement { Id = id };
                agreementDal.Delete(entity);
                await agreementDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
    }
}
