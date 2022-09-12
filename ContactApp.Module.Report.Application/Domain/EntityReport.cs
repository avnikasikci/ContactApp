using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Domain
{
    public class EntityReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public string ReportName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ReportStatus { get; set; }
        public string DataJson { get; set; }        
        public string FilePath { get; set; }
        public List<EntityReportData> Data { get; set; }
        public bool Active { get; set; }
    }
    public class EntityReportData
    {
        public string Location { get; set; }
        public string UserCount { get; set; }
        public string PhoneCount { get; set; }
        public string MailCount { get; set; }
    }
}
