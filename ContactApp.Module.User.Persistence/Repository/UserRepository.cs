using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Persistence.Context;
using System.Linq;

namespace ContactApp.Module.User.Persistence.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly PGDataUserContext _context;

        public UserRepository(PGDataUserContext context)
        {
            _context = context;
        }

        public IQueryable<EntityUser> GetAll()
        {
            return _context.EntityUsers.AsQueryable().Where(x => x.Active);
        }
        public EntityUser Add(EntityUser entityPerson)
        {

            var result=_context.EntityUsers.Add(entityPerson).Entity;
            _context.SaveChanges();

            return result;
        }
        public EntityUser Update(EntityUser entityPerson)
        {

            _context.EntityUsers.Update(entityPerson);
            _context.SaveChanges();

            return entityPerson;
        }
        public EntityUser Save(EntityUser entityPerson)
        {
            if (!string.IsNullOrEmpty(entityPerson.Id.ToString()) && entityPerson.Id > 0)
            {
                _context.EntityUsers.Update(entityPerson);

            }
            else
            {
                _context.EntityUsers.Add(entityPerson);
            }
            _context.SaveChanges();

            return entityPerson;
        }

        public EntityUser SelectById(int Id)
        {
            var Entity = _context.EntityUsers.FirstOrDefault(x => x.Id == Id);
            return Entity;
        }

    }
}
