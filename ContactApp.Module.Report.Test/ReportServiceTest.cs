

using ContactApp.Module.Report.Application.Domain;
using ContactApp.Module.Report.Persistence.Context;
using ContactApp.Module.Report.Persistence.Repostiory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContactApp.Module.Report.Test
{

    public class ReportServiceTest
    {
        private readonly DbContextOptions<PGDataReportContext> _dbOptions;

        public ReportServiceTest()
        {
            _dbOptions = new DbContextOptionsBuilder<PGDataReportContext>().UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new PGDataReportContext(_dbOptions))
            {
                dbContext.AddRange(GetFakeReport());
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async void Create_Should_Insert_New_Report()
        {
            var dbContext = new PGDataReportContext(_dbOptions);
            var reportRepository = new ReportRepository(dbContext);

            var dataId = 0;
            // Act
            var saveEntity = new EntityReport(dataId, "test1", new DateTime(), new DateTime(), 1, "", "", new(), true);
            saveEntity.setData(new List<EntityReportData>() { new EntityReportData { Location = "localation" } });
            var result = reportRepository.Save(saveEntity);
            dataId = saveEntity.Id;
            // Assert
            Assert.True(result.Id == dataId);
            Assert.True(dbContext.EntityReports.FirstOrDefaultAsync(x => x.Id == dataId) != null);
            Assert.True(GetFakeReport().Count + 1 == dbContext.EntityReports.Count());
        }
        [Fact]
        public async void GetAll_Should_Return_User()
        {
            var dbContext = new PGDataReportContext(_dbOptions);
            var reportRepository = new ReportRepository(dbContext);

            // Act
            var allReport = reportRepository.GetAll();


            // Assert
            Assert.NotEqual(0, allReport.Count());
        }
        [Fact]
        public void GetSingle_Should_Return_Any_Report_By_Id()
        {
            var dbContext = new PGDataReportContext(_dbOptions);
            var reportRepository = new ReportRepository(dbContext);

            // Act

            var saveEntity = new EntityReport(0, "test1", new DateTime(), new DateTime(), 1, "", "", new(), true);
            saveEntity.setData(new List<EntityReportData>() { new EntityReportData { Location = "localation" } });
            var resultEntity = reportRepository.Save(saveEntity);
            var saveId = resultEntity.Id;
            var entity = reportRepository.SelectById(saveId);

            // Assert
            Assert.True(resultEntity.Id == entity.Id);

        }
        [Fact]
        public void Update_Should_Update_Report()
        {
            var dbContext = new PGDataReportContext(_dbOptions);
            var reportRepository = new ReportRepository(dbContext);
            var dataId = 0;

            // Act

            var saveEntity = new EntityReport(dataId, "test1", new DateTime(), new DateTime(), 1, "", "", new(), true);
            saveEntity.setData(new List<EntityReportData>() { new EntityReportData { Location = "localation" } });
            var resultEntity = reportRepository.Save(saveEntity);

            var newName = saveEntity.ReportName + "updated";
            var saveId = resultEntity.Id;
            var newEntity = reportRepository.SelectById(saveId);
            newEntity.setReportName(newName);
            reportRepository.Save(newEntity);

            // Assert
            Assert.NotEqual(resultEntity.ReportName + "updated", newEntity.ReportName);

        }

        private List<EntityReport> GetFakeReport()
        {
            return new List<EntityReport>()
            {
                new EntityReport(0,"test1", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "konya" } },true),
                new EntityReport(0,"test2", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "ankara" } },true),
                new EntityReport(0,"test3", new DateTime(), new DateTime(),1,"","", new List<EntityReportData>() { new EntityReportData { Location = "samsun" } },true),
            };
        }


    }
}
