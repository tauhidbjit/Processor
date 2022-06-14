﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Processor.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates, int pageNumber, int pageSize);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
