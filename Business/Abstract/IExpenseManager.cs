using Business.Base;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExpenseManager
    {
        Task<Result> Add(Expense expense, IFormFile formFile);
        Task<ExpenseModel> GetModelById(int id);
        Task<TableResponseDto<ExpenseTableDto>> GetTable(TableParams param);
    }
}
