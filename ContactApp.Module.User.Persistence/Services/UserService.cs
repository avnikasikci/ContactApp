using ContactApp.Core.Application.SharedModels;
using ContactApp.Core.Persistence.Repository; 
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Enums;
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
        private readonly IUserContactInformationService _UserContactInformation;
        public UserService(
            IMongoDbRepository<EntityUser> PersonRepository,
            IUserContactInformationService UserContactInformation


            )
        {
            _UserRepository = PersonRepository;
            _UserContactInformation = UserContactInformation;

        }

        public IQueryable<EntityUser> GetAll()
        {
            return _UserRepository.All.Where(x => x.Active);
        }

        public CustomerReport GetCustomerReport(string ReportName, DateTime AddedOnDate)
        {
            var result = new CustomerReport();
            result.ReportName = ReportName;
            result.AddedOnDate = AddedOnDate;
            result.Data = new List<CustomerReportData>();

            var allPerson = this.GetAll();

            //var allLocaltion = this.GetAll().SelectMany(x => x.ContactInformations.Where(x => x.InformationType == (int)EnumCollection.ConcactType.Localation).Select(x => x.InformationDesc).ToList()).Distinct().ToList();
            //var allLocaltionS = this.GetAll().Select(x => x.ContactInformations.Where(x => x.InformationType == (int)EnumCollection.ConcactType.Localation).ToList().Select(x => x.InformationDesc)).ToList();


            //foreach (var localation in allLocaltion)
            //{
            //    var data = new CustomerReportData();
            //    var query = allPerson.Where(x => x.ContactInformations.Any(y => y.InformationDesc == localation)).ToList();
            //    var personCOunt = query.Count();
            //    var phoneCount = query.Where(x => x.ContactInformations.Any(y => y.InformationType == (int)EnumCollection.ConcactType.Phone));
            //    var mailCount = query.Where(x => x.ContactInformations.Any(y => y.InformationType == (int)EnumCollection.ConcactType.Mail));
            //    result.Data.Add(data);
            //}


            var resultQuery = (from person in allPerson.ToList()
                               join personCi in _UserContactInformation.GetAll().ToList() on person.ObjectId equals personCi.ObjectUserId
                               where personCi.InformationType == (int)EnumCollection.ConcactType.Localation
                               group personCi by personCi.InformationDesc into personConcantList
                               select new CustomerReportData
                               {
                                   Location = personConcantList.Key,
                                   PhoneCount = (from personCi in _UserContactInformation.GetAll().ToList() //personConcact connection
                                                             join person in allPerson on personCi.ObjectUserId equals person.ObjectId // join user db
                                                             where personCi.InformationType == (int)EnumCollection.ConcactType.Phone
                                                                 && (from personCi in _UserContactInformation.GetAll().ToList()
                                                                     where
                                                                           personCi.ObjectUserId == person.ObjectId && personCi.InformationDesc == personConcantList.Key
                                                                     select personCi).Any()
                                                             select personCi).Count().ToString(),
                                   UserCount = personConcantList.Count().ToString(),
                                   MailCount = (from personCi in _UserContactInformation.GetAll().ToList() //personConcact connection
                                                          join person in allPerson on personCi.ObjectUserId equals person.ObjectId // join user db
                                                          where personCi.InformationType == (int)EnumCollection.ConcactType.Mail
                                                              && (from personCi in _UserContactInformation.GetAll().ToList()
                                                                  where
                                                                        personCi.ObjectUserId == person.ObjectId && personCi.InformationDesc == personConcantList.Key
                                                                  select personCi).Any()
                                                          select personCi).Count().ToString(),

                               }).ToList();

            result.Data = resultQuery;


            return result;
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

            //entityPerson.ContactInformations.ToList().ForEach(x => x.ObjectUserId = entityPerson.ObjectId);
            //entityPerson.ContactInformations= _UserContactInformation.SaveMulti(entityPerson.ContactInformations.ToList());

            //foreach (var item in entityPerson.ContactInformations.ToList())
            //{
            //    if (!string.IsNullOrEmpty(item.ObjectId))
            //    {
            //        _ContactInformationRepository.UpdateAsync(item, x => x.ObjectId == item.ObjectId);
            //    }
            //    else
            //    {
            //        _ContactInformationRepository.AddAsync(entity: item);
            //    }


            //}
            ////_ContactInformationRepository.AddRangeAsync(entityPerson.ContactInformations);
            return entityPerson;
        }

        public EntityUser SelectById(string objectId)
        {
            var Entity = _UserRepository.All.Where(x => x.ObjectId == objectId).FirstOrDefault();
            return Entity;
        }
    }
}
