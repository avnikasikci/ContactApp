using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
namespace ContactApp.Core.Persistence.Repository
{

    public partial class PGRepository<T> : IPGRepository<T> where T : class
    {
        private readonly dynamic _context;
        private readonly DbSet<T> _dbSet;

        public PGRepository(dynamic context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> All
        {
            get { return _dbSet; }
        }

        public virtual T SelectById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public IQueryable<T> AllNonTrackable()
        {
            return _dbSet.AsNoTracking<T>();
        }
        public virtual void DeleteById(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _context.Entry(entityToDelete).State = EntityState.Modified;
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();

        }
        public virtual void DeleteMultiple(IEnumerable<T> Entities)
        {
            _dbSet.RemoveRange(Entities);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddMultipleInsert(IEnumerable<T> q)
        {
            _dbSet.AddRange(q);
        }

        public TP ExecuteQueryValue<TP>(string Query, params object[] Params)//.Net Core
        {
            return (ExecuteQueryFunc<TP>(Query, false, Params)).FirstOrDefault();
        }

        public List<TP> ExecuteQuery<TP>(string Query, params object[] Params)//.Net Core
        {
            return ExecuteQueryFunc<TP>(Query, true, Params);
        }

        private List<TP> ExecuteQueryFunc<TP>(string Query, bool IsListObject, params object[] Params) 
        {
            using (var dummyCmd = _context.Database.GetDbConnection().CreateCommand())
            {
                dummyCmd.CommandText = Query;
                dummyCmd.CommandType = System.Data.CommandType.Text;

                System.Data.Common.DbParameter[] dbParameters = Params.Select(c =>
                {
                    System.Data.Common.DbParameter par = dummyCmd.CreateParameter();
                    par.ParameterName = ((System.Data.Common.DbParameter)c).ParameterName;
                    par.Value = ((System.Data.Common.DbParameter)c).Value ?? DBNull.Value;
                    return par;
                }).ToArray();

                dummyCmd.Parameters.AddRange(dbParameters);

                var entities = new List<TP>();

                _context.Database.OpenConnection();

                using (var result = dummyCmd.ExecuteReader())
                {
                    var obj = Activator.CreateInstance<TP>();
                    Type temp = typeof(TP);
                    if (!IsListObject)
                    {
                        while (result.Read())
                        {
                            var val = result.IsDBNull(0) ? null : result[0];
                            entities.Add((TP)val);
                            break;
                        }
                    }
                    else
                    {
                        while (result.Read())
                        {

                            var newObject = Activator.CreateInstance<TP>();
                            for (var i = 0; i < result.FieldCount; i++)
                            {
                                var name = result.GetName(i);

                                
                                PropertyInfo prop = temp.GetProperties().Where(x => { var attrib = ((ColumnAttribute)x.GetCustomAttributes(typeof(ColumnAttribute), false).SingleOrDefault()); return ((attrib != null && attrib.Name.ToLower().Equals(name.ToLower())) || (attrib == null && x.Name.ToLower().Equals(name.ToLower()))); }).FirstOrDefault();                               

                                if (prop == null)
                                {
                                    continue;
                                }
                                var val = result.IsDBNull(i) ? null : result[i];


                                prop.SetValue(newObject, val, null);                                
                            }
                            
                            entities.Add(newObject);
                        }
                    }
                }

                return entities;
            }

        }

        //public IQueryable<T> RawQuery(string Query, params object[] Parameters)// context içine nesne DbSet edilmiş ise cağırılır DTO dahil.
        //{          
           

        //    return (IQueryable<T>)_dbSet.FromSqlRaw(Query, Parameters ?? new object[0]);
         
        //}

        //public IEnumerable<TP> RawQuery<TP>(string Query, params Tuple<string, object>[] Parameters)
        //{
        //    var dummyCmd = _context.Database.GetDbConnection().CreateCommand();


        //    var dbParameters = Parameters.Select(c =>
        //    {
        //        var par = dummyCmd.CreateParameter();
        //        par.ParameterName = c.Item1;
        //        par.Value = c.Item2 ?? DBNull.Value;
        //        return par;
        //    }).ToArray();            

        //    var QuerySql = (IQueryable<TP>)_dbSet.FromSqlRaw(Query, Parameters); // Net Core İçin Eklendi
        //    return QuerySql.AsEnumerable();

        //}

        public void ExecuteRawQuery(string Query, params object[] Parameters)
        {
            _context.Database.ExecuteSqlRaw(Query, Parameters ?? new object[0]);
            
        }
        public void ExecuteRawQueryDoNotEnsureTransaction(string Query, params object[] Parameters)
        {
            _context.Database.ExecuteSqlRawAsync(Query, Parameters ?? new object[0]);            
        }

        #region //For Bulk Update
        public string ConnectionStr()
        {
            return _context.Database.GetConnectionString();

        }
        public string GetTableName()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));            
            var tableName = entityType.GetTableName();
            return tableName;            
        }
        //public string GetTableNameWithShema()
        //{
        //    var entityType = _context.Model.FindEntityType(typeof(T));
        //    var schemaAnnotation = entityType.GetAnnotations().FirstOrDefault(a => a.Name == "Relational:Schema");
        //    var schema = schemaAnnotation == null ? "dbo" : schemaAnnotation.Value.ToString();
        //    var tableName = entityType.GetTableName();
        //    return $"{schema}.{tableName}";
        //}
        #endregion //For Bulk Update

        public void SetConnectionTimeout(int ConnectionTimeout)
        {
            _context.Database.SetCommandTimeout(ConnectionTimeout); // Default 30 yani 30sn
        }

    }

}
