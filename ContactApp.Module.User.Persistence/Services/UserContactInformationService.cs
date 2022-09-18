using ContactApp.Core.Application.SharedModels;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Enums;
using ContactApp.Module.User.Application.Services.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Services
{

    public class UserContactInformationService : IUserContactInformationService
    {
        private readonly IMongoDbRepository<EntityUserContactInformation> _contactInformationRepository;
        public UserContactInformationService(

            IMongoDbRepository<EntityUserContactInformation> contactInformationRepository


            )
        {

            _contactInformationRepository = contactInformationRepository;

        }

        public IQueryable<EntityUserContactInformation> GetAll()
        {
            return _contactInformationRepository.All;
        }
        public List<EntityUserContactInformation> SaveMultible(List<EntityUserContactInformation> entityListContact)
        {
            foreach (var item in entityListContact)
            {
                this.Save(item);
            }
            return entityListContact;
        }


        public EntityUserContactInformation Save(EntityUserContactInformation entityPerson)
        {
            if (!string.IsNullOrEmpty(entityPerson.ObjectId))
            {
                _contactInformationRepository.UpdateAsync(entityPerson, x => x.ObjectId == entityPerson.ObjectId);

            }
            else
            {
                _contactInformationRepository.AddAsync(entity: entityPerson);
            }

            return entityPerson;
        }

        public EntityUserContactInformation SelectById(string objectId)
        {
            var entity = _contactInformationRepository.All.Where(x => x.ObjectId == objectId).FirstOrDefault();
            return entity;
        }
        public Task<bool> SaveSpecial(List<EntityUserContactInformation> dataList)
        {
            // create a filter
            var userIdFilter = Builders<EntityUserContactInformation>.Filter
                .And(
                    Builders<EntityUserContactInformation>.Filter.Eq(person => person.ObjectUserId, dataList.FirstOrDefault().ObjectUserId)
                    );
            var personsDeleteResult = _contactInformationRepository.DeleteManyAsync(userIdFilter);


            var listWrites = new List<WriteModel<EntityUserContactInformation>>();
            var insertList = dataList.Where(x => x.ObjectId == "" || x.ObjectId == null).ToList();
            var updateList = dataList.Where(x => x.ObjectId != null && x.ObjectId != null && x.ObjectId.Length == 24).ToList();

            if (insertList != null && insertList.Count > 0)
            {
                foreach (var _Insert in insertList)
                {
                    listWrites.Add(new InsertOneModel<EntityUserContactInformation>(_Insert));
                }
            }
            if (updateList != null && updateList.Count > 0)
            {
                foreach (var _Update in updateList)
                {
                    var upsertOne = new ReplaceOneModel<EntityUserContactInformation>(
                        Builders<EntityUserContactInformation>.Filter.Where(x => x.ObjectId == _Update.ObjectId), _Update)
                    { IsUpsert = true };
                    listWrites.Add(upsertOne);
                }
            }
            var result = _contactInformationRepository.AddRangeModelAsync(listWrites);
            return result;
        }
    }
}
