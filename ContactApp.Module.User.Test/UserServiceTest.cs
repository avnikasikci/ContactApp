

using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Services.Interfaces;
using ContactApp.Module.User.Application.Services;
using System.Linq;
using Xunit;

namespace ContactApp.Module.User.Test
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        public UserServiceTest()
        {
            var DbProvider = new MongoDbSettings
            { Collection = "", ConnectionString = "mongodb://localhost:27017", Database = "DbContactReport" };
            var repositoryBudgetExpense = new MongoDbRepository<EntityUser>(DbProvider);

            _userService = new UserService(repositoryBudgetExpense);
        }
        #region Property  
        //public Mock<IReportService> mockReportService = new Mock<IReportService>();
        #endregion

        [Fact]
        public async void GetAll_Should_Return_Report()
        {
            var AllReport = _userService.GetAll().ToList();
            Xunit.Assert.NotEqual(0, AllReport.Count);
        }
        [Fact]
        public void GetSingle_Should_Return_Any_Report_By_Id()
        {
            string objectId = "6320a0b35b1bcf29354e2700";
            EntityUser report = _userService.SelectById(objectId);
            Assert.NotNull(report);
            Assert.Equal("Test V1", report.ReportName);
        }
        [Fact]
        public void Create_Should_Insert_New_Report()
        {
            EntityUser report = new EntityUser();
            report.ReportName = "Test Report";
            var inserted = _userService.Save(report);
            Assert.NotEqual("", inserted.ObjectId);
            Assert.Equal(24, inserted.ObjectId.Length);
        }

        [Fact]
        public void Delete_Should_Remove_Report()
        {

        }
        [Fact]
        public void Update_Should_Update_Report()
        {

        }

    }

}
