using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Persistence.Repository
{
    public interface IPGRepository<T> where T : class
    {

        /// <summary>
        /// All record
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All { get; }
        IQueryable<T> AllNonTrackable();
        /// <summary>
        /// find record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T SelectById(int id);

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// update
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(T entityToUpdate);

        /// <summary>
        /// delete record
        /// </summary>
        /// <param name="id">record id</param>
        void DeleteById(int id);

        /// <summary>
        /// delete record
        /// </summary>
        /// <param name="entityToDelete">record</param>
        void Delete(T entityToDelete);
        void DeleteMultiple(IEnumerable<T> Entities);

        void SaveChanges();
        void AddMultipleInsert(IEnumerable<T> q);

        TP ExecuteQueryValue<TP>(string Query, params object[] Params);//.net Core
        List<TP> ExecuteQuery<TP>(string Query, params object[] Params); //.net Core 

        void ExecuteRawQuery(string Query, params object[] Parameters);
        void ExecuteRawQueryDoNotEnsureTransaction(string Query, params object[] Parameters);

        //IQueryable<T> RawQuery(string Query, params object[] Parameters);// Called if the DbSet object is included in the context, including the DTO.

        //string ConnectionStr();
        //string GetTableName();
        //string GetTableNameWithShema();

        void SetConnectionTimeout(int ConnectionTimeout);

    }

}
