using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Persistence.Context;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Persistence.Repository
{

    public class UserContactInformationRepository : IUserContactInformationRepository
    {
        private readonly PGDataUserContext _context;

        public UserContactInformationRepository(PGDataUserContext context)
        {
            _context = context;
        }

        public IQueryable<EntityUserContactInformation> GetAll()
        {
            return _context.EntityUserContactInformations.AsQueryable();
        }

        public EntityUserContactInformation Save(EntityUserContactInformation entityPerson)
        {
            if (!string.IsNullOrEmpty(entityPerson.Id.ToString()))
            {
                _context.EntityUserContactInformations.Update(entityPerson);

            }
            else
            {
                _context.EntityUserContactInformations.Add(entityPerson);
            }
            _context.SaveChanges();
            return entityPerson;
        }

        public EntityUserContactInformation Add(EntityUserContactInformation entityPerson)
        {
            _context.EntityUserContactInformations.Add(entityPerson);
            _context.SaveChanges();
            return entityPerson;
        }

        public EntityUserContactInformation Update(EntityUserContactInformation entityPerson)
        {
            _context.EntityUserContactInformations.Update(entityPerson);
            _context.SaveChanges();
            return entityPerson;
        }

        public EntityUserContactInformation SelectById(string objectId)
        {
            var Entity = _context.EntityUserContactInformations.FirstOrDefault(x => x.Id.ToString() == objectId);
            return Entity;
        }
        public void Delete(EntityUserContactInformation entity)
        {
            _context.EntityUserContactInformations.Remove(entity);
            _context.SaveChanges();

        }

        #region  Bulk Data  
        public async Task AddBulkDataAsync(List<EntityUserContactInformation> dataList)
        {
            await _context.BulkInsertAsync(dataList);
            _context.SaveChanges();
        }
        public async Task UpdateBulkDataAsync(List<EntityUserContactInformation> dataList)
        {

            await _context.BulkUpdateAsync<EntityUserContactInformation>(dataList);
            _context.SaveChanges();
        }
        public async Task DeleteBulkDataAsync(List<EntityUserContactInformation> dataList)
        {
            await _context.BulkDeleteAsync<EntityUserContactInformation>(dataList);
            _context.SaveChanges();

        }

        #endregion




    }
}
