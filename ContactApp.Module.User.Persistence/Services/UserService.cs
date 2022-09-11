using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Person.Application.Domain;
using ContactApp.Module.User.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IMongoDbRepository<EntityUser> _UserRepository;
        public UserService(
            IMongoDbRepository<EntityUser> PersonRepository


            )
        {
            _UserRepository = PersonRepository;

        }

        public IQueryable<EntityUser> GetAll()
        {
            return _UserRepository.All.Where(x => x.Active);
        }
        public EntityUser Save(EntityUser entityPerson)
        {
            if (!string.IsNullOrEmpty(entityPerson.ObjectId))
            {
                _UserRepository.UpdateAsync(entityPerson, x => x.ObjectId == entityPerson.ObjectId);
            }
            else
            {
                _UserRepository.AddAsync(entity: entityPerson);
            }
            return entityPerson;
        }

        public EntityUser SelectById(string objectId)
        {
            var Entity = _UserRepository.All.Where(x => x.ObjectId == objectId).FirstOrDefault();
            return Entity;
        }
    }
}
