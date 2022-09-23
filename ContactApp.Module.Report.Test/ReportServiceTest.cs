

using ContactApp.Core.Persistence.DbProvider;
using ContactApp.Core.Persistence.Repository;
using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Application.Services;
using ContactApp.Module.Report.Application.Services.Interfaces;
using Mongo2Go;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ContactApp.Module.Report.Persistence.Context;
using ContactApp.Module.Report.Persistence.Repostiory;

namespace ContactApp.Module.Report.Test
{

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
        public async Task Create_Should_Insert_New_Report()
        {
            var dbContext = new PGDataReportContext(_dbOptions);
            var reportRepository = new ReportRepository(dbContext);

            var dataId = 0;
            // Act
            var saveEntity = new EntityReport(dataId, "test1", new DateTime(), new DateTime(), 1, "", "", new(), true);
            saveEntity.setData(new List<EntityReportData>() { new EntityReportData { Location = "localation" } });
            var result = reportRepository.Save(saveEntity);
            
            // Assert
            Assert.True(result.Id == dataId);
            Assert.True(dbContext.EntityReports.FirstOrDefaultAsync(x => x.Id == dataId) != null);
            Assert.True(GetFakeReport().Count == dbContext.EntityReports.Count());
        }
        private List<EntityReport> GetFakeReport()
        {
            //var result = new List<EntityReport>();            
            return new List<EntityReport>()
            {
                //new EntityReport(Guid.NewGuid(),"test1", new DateTime(), new DateTime(),1,"","",true){ Data=new List<EntityReportData>(){new EntityReportData { Id=Guid.NewGuid(), Location="ankara",} } },
                //new EntityReport(Guid.NewGuid(),"test2", new DateTime(), new DateTime(),1,"","",true){ Data=new List<EntityReportData>(){new EntityReportData { Id=Guid.NewGuid(), Location="konya",} } },
                //new EntityReport(Guid.NewGuid(),"test3", new DateTime(), new DateTime(),1,"","",true){ Data=new List<EntityReportData>(){new EntityReportData { Id=Guid.NewGuid(), Location="istanbul",} } },
                new EntityReport(0,"test1", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "konya" } },true),
                new EntityReport(0,"test2", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "ankara" } },true),
                new EntityReport(0,"test3", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "samsun" } },true),
                
                //new(Guid .NewGuid(), "Test2"),
                //new(Guid.NewGuid(), "Test3")
            };
        }


    }
}
