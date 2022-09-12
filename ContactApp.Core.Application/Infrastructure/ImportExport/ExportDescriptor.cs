using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.Infrastructure.ImportExport
{
    public class ExportDescriptor<T>
    {
        public List<ExportDescriptorItem<T>> Items { get; set; }

        public ExportDescriptor()
        {
            this.Items = new List<ExportDescriptorItem<T>>();
        }
    }
    public class ExportDescriptorItem<T>
    {
        public string Title { get; set; }
        public string Format { get; set; }
        public Expression<Func<T, object>> Expression { get; set; }
    }
}
