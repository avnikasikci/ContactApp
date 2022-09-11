using ContactApp.Core.Persistence.DbProvider;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Persistence.Repository
{
    public class MongoDbRepository<T> : IMongoDbRepository<T> where T : class
    {

        private readonly IMongoCollection<T> _context;
        private readonly IMongoCollection<T> Collection;
        private readonly MongoDbSettings settings;


        public MongoDbRepository(IMongoDbSettings settings)
        {

            //MongoClient client = new MongoClient("http://localhost:27017/");
            MongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _context = db.GetCollection<T>(this.GetTableName());
            Collection = _context;
            //BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());


            ///ssh bağşantıları için
            /// 
            /// 
            //using (var client2 = new SshClient("46.101.205.217", 22, "root", "28a07b19c80dDd")) //Here defination
            //{
            //    client2.Connect();
            //    var connectionString = "mongodb://localhost:27017";
            //    //var connectionString = "mongodb://46.101.205.217:27017";
            //    MongoClient MongoClient = new MongoClient(connectionString);
            //    IAsyncCursor<BsonDocument> lst11 = MongoClient.ListDatabases();
            //    var db2 = MongoClient.GetDatabase(settings.Database); // DbBudgetManager
            //    _context = db2.GetCollection<T>(this.GetTableName());
            //    Collection = _context;
            //    //client2.Disconnect();
            //}

        }
        public virtual IQueryable<T> All
        {
            get
            {
                var Listed = _context.Find(t => true).ToList();
                return Listed.AsQueryable();
            }
        }

        public IQueryable<T> AllNonTrackable()
        {
            throw new NotImplementedException();
        }

        public T SelectById(int id)
        {
            throw new NotImplementedException();
        }


        public void AddMultipleInsert(IEnumerable<T> q)
        {
            throw new NotImplementedException();
        }

        public void ExecuteRawQuery(string Query, params object[] Parameters)
        {
            throw new NotImplementedException();
        }

        public void ExecuteRawQueryDoNotEnsureTransaction(string Query, params object[] Parameters)
        {
            throw new NotImplementedException();
        }

        public List<T1> RawQuery<T1>(string Query, params object[] Params)
        {
            throw new NotImplementedException();
        }

        public string ConnectionStr()
        {
            throw new NotImplementedException();
        }

        public void Delete(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMultiple(IEnumerable<T> Entities)
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            return typeof(T).Name;
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToUpdate)
        {
            _context.ReplaceOne(t => true, entityToUpdate);

            throw new NotImplementedException();
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? Collection.AsQueryable()
                : Collection.AsQueryable().Where(predicate);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        //public virtual Task<T> GetByIdAsync(string id)
        //{
        //    return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            //await Collection.InsertOneAsync(entity, options);
            await _context.InsertOneAsync(entity, options);
            return entity;
        }
        public virtual async Task<bool> AddRangeModelAsync(IEnumerable<WriteModel<T>> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync(entities, options)).IsAcknowledged;
        }
        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options)).IsAcknowledged;
        }

        //public virtual async Task<T> UpdateAsync(string id, T entity)
        //{
        //    return await Collection.FindOneAndReplaceAsync(x => x.ObjectId == id, entity);
        //}

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        //public virtual async Task<T> DeleteAsync(T entity)
        //{
        //    return await Collection.FindOneAndDeleteAsync(x => x.ObjectId == entity.ObjectId);
        //}

        //public virtual async Task<T> DeleteAsync(string id)
        //{
        //    return await Collection.FindOneAndDeleteAsync(x => x.ObjectId == id);
        //}

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.FindOneAndDeleteAsync(filter);
        }


    }

}
