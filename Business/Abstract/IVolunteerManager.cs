using Business.Base;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVolunteerManager : IBaseManager<Volunteer, VolunteerModel>
    {
        Task<TableResponseDto<VolunteerListDto>> GetTable(VolunteerTableParamsDto param);
        Task<VolunteerDto> GetByIdAsync(int id);
        Task<Result> AddWithMail(VolunteerModel model);
        Task<ResultData<Volunteer>> Approve(Volunteer volunteer);
        Task<Result> Cancel(int id, string cancellationReason);
        Task<Result> Delete(int id);
        Task<Result> ApproveAndCheckMail(int id);
        Task<Result> UploadDocuments(VolunteerDocumentPostModel model);
        Task<VolunteerDetailDto> GetDetailDto(int id);
    }
}
