using ContactApp.Core.Application.SharedModels;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Person.Application.Domain;
using ContactApp.Module.Person.Application.Enums;
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
        private readonly IMongoDbRepository<EntityUserContactInformation> _ContactInformationRepository;
        public UserContactInformationService(

            IMongoDbRepository<EntityUserContactInformation> ContactInformationRepository


            )
        {

            _ContactInformationRepository = ContactInformationRepository;

        }

        public IQueryable<EntityUserContactInformation> GetAll()
        {
            return _ContactInformationRepository.All;
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
                _ContactInformationRepository.UpdateAsync(entityPerson, x => x.ObjectId == entityPerson.ObjectId);

            }
            else
            {
                _ContactInformationRepository.AddAsync(entity: entityPerson);
            }

            return entityPerson;
        }

        public EntityUserContactInformation SelectById(string objectId)
        {
            var Entity = _ContactInformationRepository.All.Where(x => x.ObjectId == objectId).FirstOrDefault();
            return Entity;
        }
        public void Delete(EntityUserContactInformation objectEntity)
        {
            //var entity = this.SelectById(objectId);
            _ContactInformationRepository.DeleteAsync(objectEntity);
        }

        public Task<bool> SaveSpecial(List<EntityUserContactInformation> DataList)
        {

            // create a filter
            var userIdFilter = Builders<EntityUserContactInformation>.Filter
                .And(
                    Builders<EntityUserContactInformation>.Filter.Eq(person => person.ObjectUserId, DataList.FirstOrDefault().ObjectUserId)
                    );
            var personsDeleteResult = _ContactInformationRepository.DeleteManyAsync(userIdFilter);


            var listWrites = new List<WriteModel<EntityUserContactInformation>>();
            var InsertList = DataList.Where(x => x.ObjectId == "" || x.ObjectId == null).ToList();
            var UpdateList = DataList.Where(x => x.ObjectId != null && x.ObjectId != null && x.ObjectId.Length == 24).ToList();

            if (InsertList != null && InsertList.Count > 0)
            {
                foreach (var _Insert in InsertList)
                {
                    listWrites.Add(new InsertOneModel<EntityUserContactInformation>(_Insert));
                }
            }
            if (UpdateList != null && UpdateList.Count > 0)
            {
                foreach (var _Update in UpdateList)
                {


                    var upsertOne = new ReplaceOneModel<EntityUserContactInformation>(
                        Builders<EntityUserContactInformation>.Filter.Where(x => x.ObjectId == _Update.ObjectId), _Update)
                    { IsUpsert = true };
                    listWrites.Add(upsertOne);                    
                }
            }
            var result = _ContactInformationRepository.AddRangeModelAsync(listWrites);
            return result;
        }
    }
}
