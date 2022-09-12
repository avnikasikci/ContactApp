using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.Infrastructure.ImportExport
{
    public class XlsxExportService : IExportService
    {
        public void ExportDataTable(DataTable Source, Stream Output)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].LoadFromDataTable(Source, true);
                ws.Cells[ws.Dimension.Address].AutoFilter = true;
                ws.Cells[ws.Dimension.Address].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                pck.SaveAs(Output);

            }
        }
        public void ExportList<T>(List<T> Source, ExportDescriptor<T> Descriptor, Stream Output)
        {
            using (OfficeOpenXml.ExcelPackage pck = new ExcelPackage(Output))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");

                List<Func<T, object>> compiledFuncs = new List<Func<T, object>>();
                for (int i = 0; i < Descriptor.Items.Count; i++)
                {


                    ExportDescriptorItem<T> descriptorItem = Descriptor.Items[i];
                    compiledFuncs.Add(descriptorItem.Expression.Compile());
                    ws.Cells[1, i + 1].Value = descriptorItem.Title;

                    if (!string.IsNullOrEmpty(descriptorItem.Format))
                    {
                        ws.Column(i + 1).Style.Numberformat.Format = descriptorItem.Format;
                    }

                }

                for (int row = 0; row < Source.Count; row++)
                {
                    T item = Source[row];
                    for (int col = 0; col < Descriptor.Items.Count; col++)
                    {
                        ExportDescriptorItem<T> descriptorItem = Descriptor.Items[col];
                        Func<T, object> compiledFunc = compiledFuncs[col];
                        ws.Cells[row + 2, col + 1].Value = compiledFunc.Invoke(item);
                    }
                }
                /*
                ws.Cells["A1"].LoadFromDataTable(Source, true);
                ws.Cells[ws.Dimension.Address].AutoFilter = true;
                ws.Cells[ws.Dimension.Address].Style.Numberformat.Format = "#,##0.00";
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                
                */
                ws.Cells[ws.Dimension.Address].AutoFilter = true;
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                pck.SaveAs(Output);

            }
        }


        public byte[] ExportListToByteArray<T>(List<T> Source, ExportDescriptor<T> Descriptor)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                this.ExportList(Source, Descriptor, ms);
                return ms.ToArray();
            }
        }
    }

}
