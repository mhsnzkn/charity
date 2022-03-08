using Data.Entities;
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
    public interface ICommonFileManager
    {
        Task DeleteVolunteerFile(int volunteerId);
        Task<CommonFile> UploadFile(IFormFile file, string fileName, string type);
        Task<Result> UploadVolunteerFile(Volunteer volunteer, IFormFile file, string fileName, string type);
    }
}
