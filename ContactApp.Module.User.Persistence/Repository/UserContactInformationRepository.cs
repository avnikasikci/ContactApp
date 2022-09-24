using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Persistence.Context;
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

        public async Task BulkInsert(List<EntityUserContactInformation> dataList)
        {
            await _context.EntityUserContactInformations.AddRangeAsync(dataList);
            await _context.SaveChangesAsync();
        }
        public void BulkUpdate(List<EntityUserContactInformation> dataList)
        {
            _context.EntityUserContactInformations.UpdateRange(dataList);
            _context.SaveChanges();
        }
        public void BulkDelete(List<EntityUserContactInformation> dataList)
        {
            _context.EntityUserContactInformations.RemoveRange(dataList);
            _context.SaveChanges();
        }

    }
}
