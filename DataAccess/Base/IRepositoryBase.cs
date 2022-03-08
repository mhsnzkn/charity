using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public interface IRepositoryBase<T> where T : Entity, new()
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression = null);
        void Add(T entity, DateTime? date = null);
        void Update(T entity, DateTime? date = null);
        void Delete(T entity);
        Task Save();
    }
}
