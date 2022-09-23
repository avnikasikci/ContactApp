


using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Enums;
using ContactApp.Module.User.Application.Services;
using ContactApp.Module.User.Persistence.Context;
using ContactApp.Module.User.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace ContactApp.Module.User.Test
{
    public class UserServiceTest
    {
        private readonly DbContextOptions<PGDataUserContext> _dbOptions;

        public UserServiceTest()
        {
            _dbOptions = new DbContextOptionsBuilder<PGDataUserContext>().UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new PGDataUserContext(_dbOptions))
            {
                dbContext.AddRange(GetFakeUser());
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async void Create_Should_Insert_New_Report()
        {
            var dbContext = new PGDataUserContext(_dbOptions);
            var userRepository = new UserRepository(dbContext);

            var dataId = 0;
            // Act
            var saveEntity = new EntityUser(0, "test", "lastname", "company", true);
            var result = userRepository.Save(saveEntity);
            dataId = saveEntity.Id;
            // Assert
            Assert.True(result.Id == dataId);
            Assert.True(dbContext.EntityUsers.FirstOrDefaultAsync(x => x.Id == dataId) != null);
            Assert.True(GetFakeUser().Count + 1 == dbContext.EntityUsers.Count());
        }
        [Fact]
        public async void GetAll_Should_Return_User()
        {
            var dbContext = new PGDataUserContext(_dbOptions);
            var userRepository = new UserRepository(dbContext);

            // Act
            var allReport = userRepository.GetAll();


            // Assert
            Assert.NotEqual(0, allReport.Count());
        }
        [Fact]
        public void GetSingle_Should_Return_Any_Report_By_Id()
        {
            var dbContext = new PGDataUserContext(_dbOptions);
            var userRepository = new UserRepository(dbContext);

            // Act

            var saveEntity = new EntityUser(0, "test1", "lastname", "company", true);
            var resultEntity = userRepository.Save(saveEntity);
            var saveId = resultEntity.Id;
            var entity = userRepository.SelectById(saveId);

            // Assert
            Assert.True(resultEntity.Id == entity.Id);

        }
        [Fact]
        public void Update_Should_Update_Report()
        {
            var dbContext = new PGDataUserContext(_dbOptions);
            var userRepository = new UserRepository(dbContext);
            var dataId = 0;

            // Act
            var saveEntity = new EntityUser(0, "test1", "lastname", "company", true);
            var resultEntity = userRepository.Save(saveEntity);

            var newName = saveEntity.FirstName + "updated";
            var saveId = resultEntity.Id;
            var newEntity = userRepository.SelectById(saveId);
            newEntity.setFirstName(newName);
            userRepository.Save(newEntity);

            // Assert
            Assert.NotEqual(resultEntity.FirstName + "updated", newEntity.FirstName);

        }
        [Fact]
        public void Get_User_Report_Count_Test()
        {
            // Arrange
            var dbContext = new PGDataUserContext(_dbOptions);
            var userRepository = new UserRepository(dbContext);
            var userContactInformationRepository = new UserContactInformationRepository(dbContext);

            var userContactInformationService = new UserContactInformationService(userContactInformationRepository);

            var userService = new UserService(userRepository, userContactInformationService);

            var saveUserConcactList = new List<EntityUserContactInformation>{
                new EntityUserContactInformation {  InformationDesc = "Ankara", InformationType = (int)EnumCollection.ConcactType.Localation},
                new EntityUserContactInformation {  InformationDesc = "Phone", InformationType = (int)EnumCollection.ConcactType.Phone},
                new EntityUserContactInformation {  InformationDesc = "Mail", InformationType = (int)EnumCollection.ConcactType.Mail},

            };
            var saveEntity = new EntityUser(0, "avni", "lastname", "test", true);
            saveEntity.ContactDetails = saveUserConcactList;
            userService.Save(saveEntity);

            var saveUserConcactList2 = new List<EntityUserContactInformation>{
                new EntityUserContactInformation {  InformationDesc = "Samsun", InformationType = (int)EnumCollection.ConcactType.Localation},
                new EntityUserContactInformation {  InformationDesc = "Phone2", InformationType = (int)EnumCollection.ConcactType.Phone},
                new EntityUserContactInformation {  InformationDesc = "Phone3", InformationType = (int)EnumCollection.ConcactType.Phone},
                new EntityUserContactInformation {  InformationDesc = "Mail2", InformationType = (int)EnumCollection.ConcactType.Mail},

            };
            var saveEntity2 = new EntityUser(0, "avni2", "lastname", "test", true);
            saveEntity2.ContactDetails = saveUserConcactList2;
            userService.Save(saveEntity2);

            var allUser = userService.GetAll().ToList();
            var customerReport = userService.GetCustomerReport("testReport", DateTime.Now);
            foreach (var item in customerReport.Data)
            {
                var Location = item.Location;
                var mailCount = Convert.ToInt32(item.MailCount);
                var phoneCount = Convert.ToInt32(item.PhoneCount);
                var userCount = Convert.ToInt32(item.UserCount);
                if (Location == "Ankara")//ankara mail =1,sms=1,usercount=1
                {
                    Assert.True(mailCount == 1);
                    Assert.True(phoneCount == 1);
                    Assert.True(userCount == 1);
                }else if(Location == "Samsun")
                {
                    Assert.True(mailCount == 1);
                    Assert.True(phoneCount == 2);
                    Assert.True(userCount == 1);
                }

            }//Ankara mail=1,Phone=1,Usercount=1
        }

        private List<EntityUser> GetFakeUser()
        {
            return new List<EntityUser>()
            {
               new EntityUser(0,"test1","lastname","company", true),
                new EntityUser(0,"test2","lastname","company", true),
                new EntityUser(0,"test3","lastname","company", true)
            };
        }


    }

}
