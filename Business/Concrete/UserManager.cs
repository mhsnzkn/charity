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
using Data.Dtos.Datatable;

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

        public async Task<Result> Add(UserEditModel model)
        {
            var result = new Result();
            if (string.IsNullOrEmpty(model.Password))
            {
                result.SetError(UserMessages.DataNotFound);
                return result;
            }
            var emailCheckEntity = await userDal.GetByMail(model.Email);
            if(emailCheckEntity is not null)
            {
                result.SetError(UserMessages.EmailExists);
                return result;
            }
            try
            {
                var entity = mapper.Map<User>(model);
                SecurityHelper.CreatePasswordHash(model.Password, out var hash, out var salt);
                entity.PasswordHash = hash;
                entity.PasswordSalt = salt;
                entity.CrtDate = DateTime.Now;
                userDal.Add(entity);
                await userDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
        }
        public async Task<Result> Update(UserEditModel model)
        {
            var result = new Result();
            if(model.Id == 1 && model.Status == Enums.UserStatus.Pasive)
            {
                result.SetError(UserMessages.UnauthorizedAccess);
                return result;
            }
            try
            {
                var entity = await userDal.GetByIdAsync(model.Id);
                entity.Name = model.Name;
                entity.Email = model.Email;
                entity.Status = model.Status;
                entity.Job = model.Job;
                entity.Role = model.Role;
                entity.UptDate = DateTime.Now;
                await userDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
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
            if(user.Status != Enums.UserStatus.Active)
            {
                result.SetError(UserMessages.UserNotActive);
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

        public async Task<TableResponseDto<UserListDto>> GetTable(UserTableParamsDto param)
        {
            var query = userDal.Get();

            if (!string.IsNullOrEmpty(param.SearchString))
                query = query.Where(a => a.Name.Contains(param.SearchString) ||
                                        a.Email.Contains(param.SearchString));

            var total = await query.CountAsync();
            if (param.Length > 0)
            {
                query = query.Skip(param.Start).Take(param.Length);
            }

            var tableModel = new TableResponseDto<UserListDto>()
            {
                Records = await mapper.ProjectTo<UserListDto>(query).ToListAsync(),
                TotalItems = total,
                PageIndex = (param.Start / param.Length) + 1
            };

            return tableModel;
        }

        public async Task<UserEditModel> GetUser(int userId)
        {
            return mapper.Map<UserEditModel>(await userDal.Get(a => a.Id == userId).FirstOrDefaultAsync());
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            try
            {
                var entity = new User() { Id = id };
                userDal.Delete(entity);
                await userDal.Save();
            }
            catch (Exception)
            {
                result.SetError(UserMessages.Fail);
            }
            return result;
        }
    }
}
