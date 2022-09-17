using ContactApp.Core.Application.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Domain
{
    public class EntityReport : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; private set; }
        public string ReportName { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public int ReportStatus { get; private set; }
        public string DataJson { get; private set; }
        public string FilePath { get; private set; }
        public List<EntityReportData> Data { get; private set; }
        public bool Active { get; private set; }

        //public string ObjectId { get; set; }
        //public string ReportName { get; set; }
        //public DateTime CreateTime { get; set; }
        //public DateTime UpdateTime { get; set; }
        //public int ReportStatus { get; set; }
        //public string DataJson { get; set; }        
        //public string FilePath { get; set; }
        //public List<EntityReportData> Data { get; set; }
        //public bool Active { get; set; }
        public EntityReport(string objectId, string reportName, DateTime createTime, DateTime updateTime, int reportStatus, string dataJson, string filePath, List<EntityReportData> data, bool active)
        {
            this.ObjectId = objectId;
            this.ReportName = reportName;
            this.CreateTime = createTime;
            this.UpdateTime = updateTime;
            this.ReportStatus = reportStatus;
            this.DataJson = dataJson;
            this.FilePath = filePath;
            this.Data = data;
            this.Active = active;
        }
        public void setObjectId(string objectId)
        {
            this.ObjectId = objectId;

        }
        public void setReportStatus(int reportStatus)
        {
            this.ReportStatus = reportStatus;
        }
        public void setFilePath(string filePath)
        {
            this.FilePath = filePath;
        }
        public void setData(List<EntityReportData> data)
        {
            this.Data = data;
        }
        public void setActive(bool active)
        {
            this.Active = active;
        }

    }
    public class EntityReportData
    {
        public string Location { get; set; }
        public string UserCount { get; set; }
        public string PhoneCount { get; set; }
        public string MailCount { get; set; }
    }
}
