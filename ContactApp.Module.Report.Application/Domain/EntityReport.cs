using ContactApp.Core.Application.Core;
using ContactApp.Core.Application.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Report.Application.Domain
{
    public class EntityReport : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ReportName { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public int ReportStatus { get; private set; }
        public string DataJson { get; private set; }
        public string FilePath { get; private set; }
        [NotMapped]
        public virtual IList<EntityReportData> Data
        {
            get => (UtilityJson.JsonDeserialize<IList<EntityReportData>>(DataJson));
            set { DataJson = UtilityJson.JsonSerialize(value); }
        }
        public bool Active { get; private set; }

        protected EntityReport()
        {

        }


        public EntityReport(int id, string reportName, DateTime createTime, DateTime updateTime, int reportStatus, string dataJson, string filePath, List<EntityReportData> data, bool active = true)
        {
            this.Id = id;
            this.ReportName = reportName;
            this.CreateTime = createTime;
            this.UpdateTime = updateTime;
            this.ReportStatus = reportStatus;
            this.DataJson = dataJson;
            this.FilePath = filePath;
            this.Data = data ?? new();
            this.Active = active;
        }

        public void setId(int id)
        {
            this.Id = id;
        }
        //
        public void setReportName(string ReportName)
        {
            this.ReportName = ReportName;
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
