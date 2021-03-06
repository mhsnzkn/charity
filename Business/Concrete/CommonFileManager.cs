using Business.Abstract;
using Data.Constants;
using Data.Entities;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommonFileManager : ICommonFileManager
    {
        private readonly ICommonFileDal commonFileDal;
        private readonly IVolunteerFileDal volunteerFileDal;

        public CommonFileManager(ICommonFileDal commonFileDal, IVolunteerFileDal volunteerFileDal)
        {
            this.commonFileDal = commonFileDal;
            this.volunteerFileDal = volunteerFileDal;
        }
        private static readonly string BasePath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build");
        public async Task<CommonFile> UploadFile(IFormFile file, string fileName, string type)
        {
            CommonFile commonFile = null;
            try
            {
                var uploadpath = Path.Combine("\\uploads", type);
                if (!Directory.Exists(uploadpath))
                    Directory.CreateDirectory(uploadpath);

                var fileFullName = fileName+ Path.GetExtension(file.FileName);
                uploadpath = Path.Combine(uploadpath, fileFullName);
                using (var fs = new FileStream(BasePath + uploadpath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                commonFile = new CommonFile()
                {
                    Type = type,
                    Path = uploadpath
                };
                commonFileDal.Add(commonFile);
            }
            catch (Exception ex)
            {
                // TODO: Log
            }

            return commonFile;
        }

        public async Task<Result> UploadVolunteerFile(Volunteer volunteer, IFormFile file, string fileName, string type)
        {
            var result = new Result();
            var commonFile = await UploadFile(file, fileName, type);
            if (commonFile == null)
                return result.SetError(UserMessages.FileUploadFailed);

            var volunteerFile = new VolunteerFile
            {
                Volunteer = volunteer,
                CommonFile = commonFile
            };
            volunteerFileDal.Add(volunteerFile);
            await volunteerFileDal.Save();

            return result;
        }

        public async Task<Result> UploadVolunteerFile(int volunteerId, IFormFile file, string fileName, string type)
        {
            var result = new Result();
            var commonFile = await UploadFile(file, fileName, type);
            if (commonFile == null)
                return result.SetError(UserMessages.FileUploadFailed);

            var volunteerFile = new VolunteerFile
            {
                VolunteerId = volunteerId,
                CommonFile = commonFile
            };
            volunteerFileDal.Add(volunteerFile);
            await volunteerFileDal.Save();

            return result;
        }

        public async Task DeleteVolunteerFile(int volunteerId)
        {
            try
            {
                var volunteerFiles = await volunteerFileDal.Get(a => a.VolunteerId == volunteerId).Include(a=>a.CommonFile).ToListAsync();
                if (volunteerFiles is null || volunteerFiles.Count == 0)
                    return;
                foreach (var item in volunteerFiles)
                {
                    File.Delete(BasePath + item.CommonFile.Path);

                    commonFileDal.Delete(item.CommonFile);
                    volunteerFileDal.Delete(item);
                }
                await volunteerFileDal.Save();
            }
            catch (Exception ex)
            {
                // log
            }

        }
    }
}
