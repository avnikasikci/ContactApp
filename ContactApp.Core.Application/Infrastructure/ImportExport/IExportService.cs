using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.Infrastructure.ImportExport
{
    public interface IExportService
    {
        byte[] ExportListToByteArray<T>(List<T> Source, ExportDescriptor<T> Descriptor);
    }
}
