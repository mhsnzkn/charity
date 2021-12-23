using Business.Base;
using Data.Utility.Results;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Dtos.Datatable;

namespace Business.Abstract
{
    public interface IUserManager
    {
        Task<Result> Add(UserEditModel model);
        Task<Result> Update(UserEditModel model);
        Task<Result> Delete(int id);
        Task<ResultData<User>> Login(UserLoginModel model);
        string CreateAccessToken(User user);
        Task<UserAccountInfoDto> GetUserInfo(int userId);
        Task<Result> EmailChange(int userId, string email);
        Task<Result> PasswordChange(int userId, string password);
        Task<Result> JobChange(int userId, string job);
        Task<TableResponseDto<UserListDto>> GetTable(UserTableParamsDto param);
        Task<UserEditModel> GetUser(int userId);
    }
}
