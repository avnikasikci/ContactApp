using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Enums;
using ContactApp.Module.User.Application.Repository;
using ContactApp.Module.User.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactApp.Module.User.Application.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContactInformationService _userContactInformation;
        public UserService(
            IUserRepository userRepository,
            IUserContactInformationService userContactInformation
            )
        {
            _userRepository = userRepository;
            _userContactInformation = userContactInformation;

        }

        public IQueryable<EntityUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public CustomerReport GetCustomerReport(string reportName, DateTime addedOnDate)
        {
            var result = new CustomerReport();
            result.ReportName = reportName;
            result.AddedOnDate = addedOnDate;
            result.Data = new List<CustomerReportData>();

            var allPerson = this.GetAll();
            var resultQuery = (from person in allPerson.ToList()
                               join personCi in _userContactInformation.GetAll().ToList() on person.Id equals personCi.UserId
                               where personCi.InformationType == (int)EnumCollection.ConcactType.Localation
                               group personCi by personCi.InformationDesc into personConcantList
                               select new CustomerReportData
                               {
                                   Location = personConcantList.Key,
                                   PhoneCount = (from personCi in _userContactInformation.GetAll().ToList() //personConcact connection
                                                 join person in allPerson on personCi.UserId equals person.Id // join user db
                                                 where personCi.InformationType == (int)EnumCollection.ConcactType.Phone
                                                     && (from personCi in _userContactInformation.GetAll().ToList()
                                                         where
                                                               personCi.UserId == person.Id && personCi.InformationDesc == personConcantList.Key
                                                         select personCi).Any()
                                                 select personCi).Count().ToString(),
                                   UserCount = personConcantList.Count().ToString(),
                                   MailCount = (from personCi in _userContactInformation.GetAll().ToList() //personConcact connection
                                                join person in allPerson on personCi.UserId equals person.Id // join user db
                                                where personCi.InformationType == (int)EnumCollection.ConcactType.Mail
                                                    && (from personCi in _userContactInformation.GetAll().ToList()
                                                        where
                                                              personCi.UserId == person.Id && personCi.InformationDesc == personConcantList.Key
                                                        select personCi).Any()
                                                select personCi).Count().ToString(),

                               }).ToList();

            result.Data = resultQuery;


            return result;
        }
        public EntityUser Add(EntityUser entityPerson)
        {

            return _userRepository.Add(entityPerson);

            
        }
        public EntityUser Update(EntityUser entityPerson)
        {

            _userRepository.Update(entityPerson);

            return entityPerson;
        }

        public EntityUser Save(EntityUser entityPerson)
        {
            return _userRepository.Save(entityPerson);
        }

        public EntityUser SelectById(int Id)
        {
            return _userRepository.SelectById(Id);
        }
    }
}
