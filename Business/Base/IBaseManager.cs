using Data.Utility.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public interface IBaseManager<T> 
        where T : class
    {
        //Task<TEntity> GetByIdAsync(int id);
        //Task<List<T>> Get(Expression<Func<T, bool>> expression = null);
        Task<Result> Add(T entity);
        Task<Result> Update(T entity);
        // Task<Result> Delete(TEntity entity);
    }
}
