using Business.Base;
using Data.Constants;
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
    public interface IVolunteerManager
    {
        Task<TableResponseDto<VolunteerTableDto>> GetTable(VolunteerTableParamsDto param);
        Task<Result> AddWithMail(VolunteerModel model);
        Task<ResultData<Volunteer>> Approve(Volunteer volunteer);
        Task<Result> Cancel(int id, string cancellationReason);
        Task<Result> Delete(int id);
        Task<Result> ApproveAndCheckMail(int id);
        Task<Result> UploadDocuments(VolunteerDocumentPostModel model);
        Task<VolunteerDetailDto> GetDetailDto(int id);
        Task<Volunteer> GetVolunteerByKey(Guid key);
        Task<Result> Update(VolunteerModel model);
        void Update(Volunteer entity);
        void SetStatus(Volunteer volunteer, VolunteerStatus status);
        Task<Result> OnHold(int id);
        Task<Result> SendStatusMail(int volunteerId);
    }
}
