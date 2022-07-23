using AutoMapper;
using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExpenseManager : IExpenseManager
    {
        private readonly IExpenseDal expenseDal;
        private readonly IMapper mapper;

        public ICommonFileManager CommonFileManager { get; }

        public ExpenseManager(IExpenseDal expenseDal, IMapper mapper, ICommonFileManager commonFileManager)
        {
            this.expenseDal = expenseDal;
            this.mapper = mapper;
            CommonFileManager = commonFileManager;
        }

        public async Task<TableResponseDto<ExpenseTableDto>> GetTable(TableParams param)
        {
            var query = expenseDal.Get().OrderBy(a=>a.CrtDate).AsQueryable();

            if (!string.IsNullOrEmpty(param.SearchString))
                query = query.Where(a => a.Volunteer.FirstName.Contains(param.SearchString) || a.Volunteer.LastName.Contains(param.SearchString));

            var total = await query.CountAsync();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var tableModel = new TableResponseDto<ExpenseTableDto>()
            {
                Records = await mapper.ProjectTo<ExpenseTableDto>(query).ToListAsync(),
                TotalItems = total,
                PageIndex = (param.Start / param.Length) + 1
            };

            return tableModel;
        }

        public async Task<Result> Save(ExpenseModel model)
        {
            if (model.Id == 0)
                return await Add(model);
            else
                return await Update(model);

        }
        public async Task<Result> Update(ExpenseModel model)
        {
            var result = new Result();
            try
            {
                var expense = await expenseDal.GetByIdAsync(model.Id);
                expense.Details = model.Details;
                expense.ModeOfTransport = model.ModeOfTransport;
                expense.Amount = model.Amount;
                expense.Claim = model.Claim;
                expense.TotalMileage = model.TotalMileage;
                expense.Date = model.Date;
                expense.VolunteerId = model.VolunteerId;

                await expenseDal.Save();

                if (model.FormFile != null)
                {
                    await CommonFileManager.UploadVolunteerFile(expense.VolunteerId, model.FormFile, $"{expense.VolunteerId}-{expense.Id}", CommonFileTypes.Expense);
                }

            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
        }
        public async Task<Result> Add(ExpenseModel model)
        {
            var result = new Result();
            try
            {
                var expense = mapper.Map<Expense>(model);
                expense.Status = ExpenseStatus.Pending;
                expenseDal.Add(expense);
                await expenseDal.Save();

                if (model.FormFile != null)
                {
                    await CommonFileManager.UploadVolunteerFile(expense.VolunteerId, model.FormFile, $"{expense.VolunteerId}-{expense.Id}", CommonFileTypes.Expense);
                }

            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
        }

        public async Task<ExpenseModel> GetModelById(int id)
        {
            return await mapper.ProjectTo<ExpenseModel>(expenseDal.Get(a=>a.Id == id)).FirstOrDefaultAsync();
        }
    }
}
