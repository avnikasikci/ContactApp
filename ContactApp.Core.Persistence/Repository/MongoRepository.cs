using ContactApp.Core.Persistence.DbProvider;
using MongoDB.Bson;
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
            MongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _context = db.GetCollection<T>(this.GetTableName());
            Collection = _context;

        }
        
        public virtual IQueryable<T> All
        {
            get
            {
                var Listed = _context.Find(t => true).ToList();
                return Listed.AsQueryable();
            }
        }


        public string GetTableName()
        {
            return typeof(T).Name;
        }


        public void Update(T entityToUpdate)
        {
            _context.ReplaceOne(t => true, entityToUpdate);
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


        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
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
        public virtual async Task<bool> DeleteMultipleAsync(IEnumerable<string> entities)
        {
            var filter = Builders<T>.Filter.In("_id", entities);
            return (await Collection.DeleteManyAsync(filter)).IsAcknowledged;

        }
        //DeleteManyAsync
        public virtual async Task<bool> DeleteManyAsync(FilterDefinition<T> filter)
        {
            return (await Collection.DeleteManyAsync(filter)).IsAcknowledged;

        }
        //DeleteOneAsync
        public virtual async Task<bool> DeleteOneAsync(FilterDefinition<T> filter)
        {
            return (await Collection.DeleteOneAsync(filter)).IsAcknowledged;
        }
        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.FindOneAndDeleteAsync(filter);
        }

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }
     
   
    }

}
