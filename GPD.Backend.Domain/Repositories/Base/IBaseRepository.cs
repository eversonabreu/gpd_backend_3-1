using GPD.Backend.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GPD.Backend.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        TEntity GetById(long id, bool loadDependencies = true);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, TEntity defaultResult = null, bool loadDependencies = true);

        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool loadDependencies = false);

        (IEnumerable<TEntity> Data, int Count) FilterWithPagination(string expression, string sort = null, uint page = 0, uint limit = 10, bool loadDependencies = false);

        long Add(TEntity entity);

        void Update(TEntity entity);

        void DeleteById(long id);

        void DeleteMany(IEnumerable<long> ids);
    }
}
