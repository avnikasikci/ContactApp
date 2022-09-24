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
        public async Task AddBulkDataAsync(List<EntityUserContactInformation> dataList)
        {
            await _contactInformationRepository.AddBulkDataAsync(dataList);

        }
        public async Task UpdateBulkDataAsync(List<EntityUserContactInformation> dataList)
        {
            await _contactInformationRepository.UpdateBulkDataAsync(dataList);

        }
        public async Task DeleteBulkDataAsync(List<EntityUserContactInformation> dataList)
        {
            await _contactInformationRepository.DeleteBulkDataAsync(dataList);

        }
        public async Task<bool> SaveSpecial(List<EntityUserContactInformation> dataList)
        {

            var deleteRecords = this.GetAll().Where(x => x.UserId == dataList.FirstOrDefault().UserId).ToList();
            foreach (var item in deleteRecords)
            {
                _contactInformationRepository.Delete(item);
            }
    
            //if (deleteRecords != null && deleteRecords.Count > 0)
            //    await this.DeleteBulkDataAsync(deleteRecords);

            var insertRecord = dataList.Where(x => x.Id.ToString() == "" || x.Id == null || x.Id <= 0).ToList();
            foreach (var item in insertRecord)
            {
                _contactInformationRepository.Add(item);

            }
            //if (insertRecord != null && insertRecord.Count > 0)
            //    await this.AddBulkDataAsync(insertRecord);

            var updateRecord = dataList.Where(x => x.Id != null && x.Id > 0).ToList();
            foreach (var item in updateRecord)
            {
                _contactInformationRepository.Update(item);

            }
            //if (updateRecord != null && updateRecord.Count > 0)
            //    await this.UpdateBulkDataAsync(updateRecord);

            return true;
        }
    }
}
