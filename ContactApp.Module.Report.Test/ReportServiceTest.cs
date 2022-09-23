

using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Services;
using ContactApp.Module.Report.Application.Services.Interfaces;
using Mongo2Go;
using MongoDB.Driver;
using System.Linq;
using Xunit;

namespace ContactApp.Module.Report.Test
{
    //public class MongoIntegrationTest
    //{
    //    internal static MongoDbRunner _runner;
    //    internal static MongoCollection<TestDocument> _collection;

    //    internal static void CreateConnection()
    //    {
    //        _runner = MongoDbRunner.Start();

    //        MongoClient client = new MongoClient(_runner.ConnectionString);
    //        MongoDatabase database = client.GetDatabase("IntegrationTest");
    //        _collection = database.GetCollection<TestDocument>("TestCollection");
    //    }
    //}
    public class ReportServiceTest
    {
        private readonly IReportService _reportService;
        private readonly MongoDbRunner _runner;
        //internal static MongoDbRunner _runner;

        public ReportServiceTest()
        {
            _runner = MongoDbRunner.StartForDebugging();
            _runner.Import("DbContactReport", "DbContactReport", @"..\..\App_Data\DbContactReport.json", true);

            var connStr = _runner.ConnectionString;
            var DbProvider = new MongoDbSettings
            { 
                Collection = "",
                //ConnectionString = "mongodb://localhost:27017", 
                ConnectionString = connStr, 
                Database = "DbContactReport" };
            var mongoDbRepository = new MongoDbRepository<EntityReport>(DbProvider);

            _reportService = new ReportService(mongoDbRepository);

        }
        #region Property  
        //public Mock<IReportService> mockReportService = new Mock<IReportService>();
        #endregion

        [Fact]
        public void GetAll_Should_Return_Report()
        {
            var AllReport = _reportService.GetAll().ToList();    
            Assert.NotEqual(0, AllReport.Count);
        }
        [Fact]
        public void GetSingle_Should_Return_Any_Report_By_Id()
        {
            string objectId = "6320a0b35b1bcf29354e2700";
            EntityReport report = _reportService.SelectById(objectId);
            Assert.NotNull(report);
            Assert.Equal("Test V1", report.ReportName);
        }
        [Fact]
        public void Create_Should_Insert_New_Report()
        {            
            EntityReport report = new EntityReport("","testv1",System.DateTime.Now, System.DateTime.Now,1,"",null,null,false);
            //report.ReportName = "Test Report";
            var inserted = _reportService.Save(report);
            Assert.NotEqual("", inserted.ObjectId);
            Assert.Equal(24, inserted.ObjectId.Length);
        }


    }
}
