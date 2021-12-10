using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public interface IBaseBusiness<T> where T : class
    {
        //Task<T> GetByIdAsync(int id);
        //Task<List<T>> Get(Expression<Func<T, bool>> expression = null);
        //Task<Result> Add(TModel model);
        //Task<Result> Update(TModel model);
        //Task<Result> Delete(T entity);
    }
}
