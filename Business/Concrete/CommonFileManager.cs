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

        public async Task<CommonFile> UploadFile(IFormFile file, string fileName, string type)
        {
            CommonFile commonFile = null;
            try
            {
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "build");
                var extension = Path.GetExtension(file.FileName);
                var uploadpath = Path.Combine("images", "uploads", fileName + extension);
                using (var fs = new FileStream(Path.Combine(fullpath, uploadpath), FileMode.Create))
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

        public async Task DeleteVolunteerFile(int volunteerId)
        {
            try
            {
                var volunteerFiles = await volunteerFileDal.Get(a => a.VolunteerId == volunteerId).ToListAsync();
                foreach (var item in volunteerFiles)
                {
                    var commonFileToDelete = new CommonFile { Id = item.CommonFileId };
                    commonFileDal.Delete(commonFileToDelete);
                    volunteerFileDal.Delete(item);
                }
                await volunteerFileDal.Save();
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
