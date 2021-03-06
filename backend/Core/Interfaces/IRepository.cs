﻿using Core.Ordering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> Get(int pageIndex = 1, int pageSize = 1, Ordering<TEntity> ordering = null);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
