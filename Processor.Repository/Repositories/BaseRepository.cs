using Processor.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Repository.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public BaseRepository(DbContext context)
        {
            Context = context;
            Context.Configuration.LazyLoadingEnabled = true;
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates, int pageNumber, int pageSize)
        {
            return Context.Set<TEntity>().Where(predicates).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates)
        {
            return Context.Set<TEntity>().Where(predicates);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
