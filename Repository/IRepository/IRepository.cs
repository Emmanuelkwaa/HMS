using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HMS.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        Task<bool> AddAsync(T entity);
        Task<bool> RemoveAsync(int id);
        bool RemoveAsync(T entity);
        void RemoveRangeAsync(IEnumerable<T> entity);
    }
}
