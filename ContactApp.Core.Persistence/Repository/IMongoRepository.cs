using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Persistence.Repository
{
    public interface IMongoDbRepository<T> where T : class
    {

        IQueryable<T> All { get; }
        void Update(T entityToUpdate);
        string GetTableName();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AddRangeModelAsync(IEnumerable<WriteModel<T>> entities);
        Task<bool> DeleteMultipleAsync(IEnumerable<string> Entities);
        Task<bool> DeleteManyAsync(FilterDefinition<T> filter);
        Task<bool> DeleteOneAsync(FilterDefinition<T> filter);

        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);

    }

}
