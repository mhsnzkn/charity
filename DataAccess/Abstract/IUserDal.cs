﻿using Data.Entities;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepositoryBase<User>
    {
        Task<User> GetByMail(string email);
    }
}