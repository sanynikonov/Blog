﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> entities;

        public Repository(BlogContext context)
        {
            entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            return await entities.Where(predicate).AsQueryable().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var entity = await entities.FindAsync(id);
            
            if (entity != null)
                entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            entities.Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAndIncludeAsync(Expression<Func<TEntity, bool>> predicate,
            Dictionary<Expression<Func<TEntity, object>>, Expression<Func<object, object>>[]> includeThenIncludeArrayPairs)
        {
            return await Include(includeThenIncludeArrayPairs)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<TEntity> GetByIdAndIncludeAsync(int id, 
            Dictionary<Expression<Func<TEntity, object>>, Expression<Func<object, object>>[]> includeThenIncludeArrayPairs)
        {
            var query = Include(includeThenIncludeArrayPairs);
            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAndIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<TEntity> GetByIdAndIncludeAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }


        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = entities.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private IQueryable<TEntity> Include(Dictionary<Expression<Func<TEntity, object>>, Expression<Func<object, object>>[]> includeThenIncludeArrayPairs)
        {
            //Dictionary:
            //Key - include property
            //Value - array of "then include" properties

            IQueryable<TEntity> query = entities.AsNoTracking();

            foreach (var pair in includeThenIncludeArrayPairs)
            {
                var includableQuery = query.Include(pair.Key);

                foreach (var thenIncludeProperty in pair.Value)
                {
                    includableQuery = includableQuery.ThenInclude(thenIncludeProperty);
                }

                query = includableQuery;
            }

            return query;
        }
    }
}
