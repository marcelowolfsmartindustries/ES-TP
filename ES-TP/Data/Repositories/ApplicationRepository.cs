﻿using ES_TP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ES_TP.Data.Repositories
{
    public class ApplicationRepository<TEntity> : IApplicationRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> EntitySet;
        protected readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            EntitySet = context.Set<TEntity>();
            _context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity != null)
            {
                await EntitySet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                await EntitySet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }

            return entities;
        }

        public virtual async Task<TEntity> Delete(Guid id)
        {
            var entity = await FindAsync(id);

            if (entity != null)
            {
                EntitySet.Remove(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            if (entity != null)
            {
                EntitySet.Remove(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Delete(IEnumerable<TEntity> entity)
        {
            if (entity != null && entity.Count() > 0)
            {
                EntitySet.RemoveRange(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public async virtual Task<TEntity> Edit(TEntity entity)
        {
            if (entity != null)
            {
                EntitySet.Update(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Edit(IEnumerable<TEntity> entity)
        {
            if (entity != null && entity.Count() > 0)
            {
                EntitySet.UpdateRange(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<TEntity> FindAsync(Guid id) => await EntitySet.FindAsync(id);

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where) => await this.WhereIQueryable(where).FirstOrDefaultAsync();

        public IQueryable<TEntity> GetEntity() => EntitySet.AsQueryable();

        public IQueryable<TEntity> GetEntity(Expression<Func<TEntity, bool>> where) => EntitySet.Where(where);

        public IQueryable<TEntity> GetEntityAsNoTracking() => EntitySet.AsNoTracking();

        public IQueryable<TEntity> GetEntityAsNoTracking(Expression<Func<TEntity, bool>> where) => EntitySet.Where(where).AsNoTracking();

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where) => await this.WhereIQueryable(where).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetListAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool ascending = true)
            => await (ascending ? this.WhereIQueryable(where).OrderBy(order).ToListAsync() : this.WhereIQueryable(where).OrderByDescending(order).ToListAsync());

        protected IQueryable<TEntity> WhereIQueryable(Expression<Func<TEntity, bool>> where) => EntitySet.Where(where);
    }
}
