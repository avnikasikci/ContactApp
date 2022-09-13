using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.User.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Services.Interfaces
{
    public interface IUserContactInformationService
    {
        IQueryable<EntityUserContactInformation> GetAll();
        EntityUserContactInformation Save(EntityUserContactInformation entityPerson);
        Task< bool> SaveSpecial(List<EntityUserContactInformation> entityPerson);
        EntityUserContactInformation SelectById(string objectId);
    }
}
