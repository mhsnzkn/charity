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

namespace Business.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserDal userDal;
        private readonly ITokenHelper tokenHelper;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            this.userDal = userDal;
            this.tokenHelper = tokenHelper;
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
    }
}
