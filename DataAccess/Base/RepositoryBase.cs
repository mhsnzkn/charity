using Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly TContext context;
        public RepositoryBase(TContext context)
        {
            this.context = context;
        }


        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ?
                     this.context.Set<TEntity>().AsNoTracking() :
                     this.context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }


        public void Update(TEntity entity)
        {
            context.Update(entity);
        }
        public void Add(TEntity entity)
        {
            context.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
