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
        Task DeleteVolunteerFiles(int volunteerId, string type);
        Task<CommonFile> UploadSaveFile(IFormFile file, string fileName, string type);
        Task<Result> UploadVolunteerFile(int volunteerId, IFormFile file, string fileName, string type);
    }
}
