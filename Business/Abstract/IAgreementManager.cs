using Business.Base;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAgreementManager
    {
        Task<Result> Add(AgreementModel model);
        Task<Result> Delete(int id);
        Task<List<Agreement>> GetActiveAgreements();
        Task<Agreement> GetById(int id);
        Task<TableResponseDto<AgreementTableDto>> GetTable(TableParams param);
        Task<Result> SaveVolunteerAgreements(VolunteerAgreementPostModel model);
        Task<Result> Update(AgreementModel model);
    }
}
