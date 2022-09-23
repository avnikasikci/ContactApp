

using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Services.Interfaces;
using ContactApp.Module.User.Application.Services;
using System.Linq;
using Xunit;
using Mongo2Go;

namespace ContactApp.Module.User.Test
{
    public class UserServiceTest
    {
       // private readonly IUserService _userService;
       // private readonly IUserContactInformationService _UserContactInformationService;
       // private readonly MongoDbRunner _runner;

       // public UserServiceTest()
       // {
       //     _runner = MongoDbRunner.Start();
       //     _runner.Export("DbIntegrationTest", "EntityUser", @"..\..\App_Data\test.json");


       //     _runner = MongoDbRunner.StartForDebugging();
       //     //_runner.Import("DbContactReport", "DbContactReport", @"..\..\App_Data\DbContactReport.json", true);

       //     var connStr = _runner.ConnectionString;
       //     var DbProvider = new MongoDbSettings
       //     {
       //         Collection = "",
       //         //ConnectionString = "mongodb://localhost:27017", 
       //         ConnectionString = connStr,
       //         Database = "DbIntegrationTest"
       //     };

       //     //var DbProvider = new MongoDbSettings
       //     //{ Collection = "", ConnectionString = "mongodb://localhost:27017", Database = "DbContactUser" };
       //     var repositoryUser = new MongoDbRepository<EntityUser>(DbProvider);
       //     var repositoryEntityUserContactInformation = new MongoDbRepository<EntityUserContactInformation>(DbProvider);
       //     _UserContactInformationService = new UserContactInformationService(repositoryEntityUserContactInformation);
       //     _userService = new UserService(repositoryUser, _UserContactInformationService);
       // }
       //public void CreateConnection()
       // {
       //     //_runner = MongoDbRunner.Start();

       //     //MongoClient client = new MongoClient(_runner.ConnectionString);
       //     //MongoDatabase database = client.GetDatabase("IntegrationTest");
       //     //_collection = database.GetCollection<TestDocument>("TestCollection");
       // }
       // #region Property  
       // //public Mock<IUserService> mockUserService = new Mock<IUserService>();
       // #endregion

       // [Fact]
       // public async void GetAll_Should_Return_User()
       // {
       //     var AllUser = _userService.GetAll().ToList();
       //     Xunit.Assert.NotEqual(0, AllUser.Count);
       // }
       // [Fact]
       // public void GetSingle_Should_Return_Any_User_By_Id()
       // {
       //     string objectId = "6320b71fec01ec967343a411";
       //     EntityUser user = _userService.SelectById(objectId);
       //     Assert.NotNull(user);
       //     Assert.Equal("Test User", user.FirstName);
       // }
       // [Fact]
       // public void Create_Should_Insert_New_User()
       // {
       //     EntityUser user = new EntityUser("","test v1","","",true);
       //     //user.FirstName = "Test User";
       //     var inserted = _userService.Save(user);
       //     Assert.NotEqual("", inserted.ObjectId);
       //     Assert.Equal(24, inserted.ObjectId.Length);
       // }

       // [Fact]
       // public void Delete_Should_Remove_User()
       // {
       //     EntityUser user = _userService.GetAll().FirstOrDefault();
       //     string objectId = user.ObjectId;

       //     //user.Active = false;
       //     _userService.Save(user);
       //     var deletedUser = _userService.GetAll().Where(x => x.ObjectId == objectId).FirstOrDefault();
       //     Assert.Null(deletedUser);         
       // }
       // [Fact]
       // public void Update_Should_Update_User()
       // {
       //     EntityUser user = _userService.GetAll().FirstOrDefault();
       //     string objectId = user.ObjectId;

       //     //user.FirstName = user.FirstName+" updated";
       //     _userService.Save(user);
       //     var updatedUser = _userService.GetAll().Where(x => x.ObjectId == objectId).FirstOrDefault();
       //     Assert.Equal(user.FirstName , updatedUser.FirstName);

       // }

    }

}
