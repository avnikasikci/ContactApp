using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Services
{

    public class UserContactInformationService : IUserContactInformationService
    {
        private readonly IUserContactInformationRepository _contactInformationRepository;
        public UserContactInformationService(
            IUserContactInformationRepository contactInformationRepository
        )
        {
            _contactInformationRepository = contactInformationRepository;
        }

        public IQueryable<EntityUserContactInformation> GetAll()
        {
            return _contactInformationRepository.GetAll();
        }

        public EntityUserContactInformation Save(EntityUserContactInformation entityPerson)
        {
            return _contactInformationRepository.Save(entityPerson);
        }

        public EntityUserContactInformation SelectById(string objectId)
        {
            return _contactInformationRepository.SelectById(objectId);
        }

        private void Delete(EntityUserContactInformation entityPerson)
        {
            _contactInformationRepository.Delete(entityPerson);
        }
        private async Task BulkInsert(List<EntityUserContactInformation> dataList)
        {
            await _contactInformationRepository.BulkInsert(dataList);
        }
        private void BulkUpdate(List<EntityUserContactInformation> dataList)
        {
            _contactInformationRepository.BulkUpdate(dataList);
        }
        private void BulkDelete(List<EntityUserContactInformation> dataList)
        {
            _contactInformationRepository.BulkDelete(dataList);
        }
        private EntityUserContactInformation Update(EntityUserContactInformation entityPerson)
        {
            return _contactInformationRepository.Update(entityPerson);
        }
        public async Task<bool> SaveSpecial(List<EntityUserContactInformation> dataList)
        {
            var deleteRecords = this.GetAll().Where(x => x.UserId == dataList.FirstOrDefault().UserId).ToList();
            if (deleteRecords.Count > 0)
                this.BulkDelete(deleteRecords);

            var insertRecord = dataList.Where(x => x.Id == null || x.Id <= 0).ToList();
            if (insertRecord.Count > 0)
                this.BulkInsert(insertRecord);

            var updateRecord = dataList.Where(x => x.Id != null && x.Id > 0).ToList();
            if (updateRecord.Count > 0)
                this.BulkUpdate(updateRecord);
            return true;
        }
    }
}
