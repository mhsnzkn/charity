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
        Task<Result> Save(ExpenseModel model);
        Task<ExpenseModel> GetModelById(int id);
        Task<TableResponseDto<ExpenseTableDto>> GetTable(ExpenseTableParamsDto param);
        Task<Result> Approve(int id);
        Task<Result> Pay(int id, DateTime date);
        Task<Result> Cancel(int id, string cancellationReason);
    }
}
