using ContactApp.Module.User.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Repository
{
    public interface IUserContactInformationRepository
    {
        IQueryable<EntityUserContactInformation> GetAll();
        EntityUserContactInformation Save(EntityUserContactInformation entityPerson);
        EntityUserContactInformation Add(EntityUserContactInformation entityPerson);
        EntityUserContactInformation Update(EntityUserContactInformation entityPerson);
        EntityUserContactInformation SelectById(string objectId);
        void Delete(EntityUserContactInformation entity);
        //Task BulkUpdateAsync(List<EntityUserContactInformation> datalist);
        Task AddBulkDataAsync(List<EntityUserContactInformation> dataList);
        Task UpdateBulkDataAsync(List<EntityUserContactInformation> dataList);
        Task DeleteBulkDataAsync(List<EntityUserContactInformation> dataList);
    }
}
