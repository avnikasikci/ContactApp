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
        T SelectById(int id);

        IQueryable<T> All { get; }
        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(T entityToUpdate);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="id">Kayıt id</param>
        void DeleteById(int id);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        void Delete(T entityToDelete);
        void DeleteMultiple(IEnumerable<T> Entities);

        void SaveChanges();
        void AddMultipleInsert(IEnumerable<T> q);

        void ExecuteRawQuery(string Query, params object[] Parameters);
        void ExecuteRawQueryDoNotEnsureTransaction(string Query, params object[] Parameters);
        List<T> RawQuery<T>(string Query, params object[] Params);
        string ConnectionStr();
        string GetTableName();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        //Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AddRangeModelAsync(IEnumerable<WriteModel<T>> entities);

        //Task<T> UpdateAsync(TKey id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        //Task<T> DeleteAsync(TKey id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);

    }

}
