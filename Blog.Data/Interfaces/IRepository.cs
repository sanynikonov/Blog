using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);

        Task<IEnumerable<TEntity>> GetAndIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAndIncludeAsync(Expression<Func<TEntity, bool>> predicate,
            Dictionary<Expression<Func<TEntity, object>>, Expression<Func<object, object>>[]> includeThenIncludeArrayPairs);

        Task<TEntity> GetByIdAndIncludeAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAndIncludeAsync(int id,
            Dictionary<Expression<Func<TEntity, object>>, Expression<Func<object, object>>[]> includeThenIncludeArrayPairs);

        Task AddAsync(TEntity entity);
        Task RemoveByIdAsync(int id);
        void Update(TEntity entity);
    }
}
