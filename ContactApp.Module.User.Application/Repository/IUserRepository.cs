using ContactApp.Module.User.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Repository
{
    public interface IUserRepository
    {
        IQueryable<EntityUser> GetAll();
        EntityUser Save(EntityUser entityPerson);
        EntityUser Add(EntityUser entityPerson);
        EntityUser Update(EntityUser entityPerson);
        EntityUser SelectById(string objectId);
    }
}
