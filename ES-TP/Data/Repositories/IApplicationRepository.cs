using ES_TP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ES_TP.Data.Repositories
{
    public interface IApplicationRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> FindAsync(Guid id);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> GetEntity();

        IQueryable<TEntity> GetEntity(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> GetEntityAsNoTracking();

        IQueryable<TEntity> GetEntityAsNoTracking(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> GetListAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);

        Task<TEntity> Edit(TEntity entity);

        Task<IEnumerable<TEntity>> Edit(IEnumerable<TEntity> entity);

        Task<TEntity> Delete(Guid id);

        Task<TEntity> Delete(TEntity entity);

        Task<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entity);
    }
}
