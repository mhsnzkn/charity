using Business.Abstract;
using Data.Utility.Results;
using Data.Utility.Security;
using Data.Constants;
using Data.Entities;
using Data.Models;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserDal userDal;
        private readonly ITokenHelper tokenHelper;
        private readonly IMapper mapper;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper, IMapper mapper)
        {
            this.userDal = userDal;
            this.tokenHelper = tokenHelper;
            this.mapper = mapper;
        }

        public Task<Result> Add(User model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultData<User>> Login(UserLoginModel model)
        {
            var result = new ResultData<User>();
            var user = await userDal.GetByMail(model.Email);
            if(user == null)
            {
                result.SetError(UserMessages.LoginFail);
                return result;
            }

            var isVerified = SecurityHelper.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt);
            if (!isVerified)
            {
                result.SetError(UserMessages.LoginFail);
                return result;
            }

            result.Data = user;
            return result;
        }
        public string CreateAccessToken(User user)
        {
            return tokenHelper.CreateToken(user);
        }

        public async Task<UserAccountInfoDto> GetUserInfo(int userId)
        {
            return await mapper.ProjectTo<UserAccountInfoDto>(userDal.Get(a => a.Id == userId)).FirstOrDefaultAsync();
        }
        public async Task<Result> EmailChange(int userId, string email)
        {
            var result = new Result();
            var entity = await userDal.GetByIdAsync(userId);
            if(entity == null || string.IsNullOrEmpty(email))
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            try
            {
                entity.Email = email;
                await userDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> PasswordChange(int userId, string password)
        {
            var result = new Result();
            var entity = await userDal.GetByIdAsync(userId);
            if (entity == null || string.IsNullOrEmpty(password))
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            try
            {
                SecurityHelper.CreatePasswordHash(password, out var hash, out var salt);
                entity.PasswordHash = hash;
                entity.PasswordSalt = salt;
                await userDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
        public async Task<Result> JobChange(int userId, string job)
        {
            var result = new Result();
            var entity = await userDal.GetByIdAsync(userId);
            if (entity == null)
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            try
            {
                entity.Job = job;
                await userDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(UserMessages.Fail);
            }

            return result;
        }
    }
}
