using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IExportRepository
    {
        Task<ExportResult> ExportToCsvAsync<T>(IEnumerable<T> data, string fileName = "export");
        Task<ExportResult> ExportToExcelAsync<T>(IEnumerable<T> data, string sheetName = "Data", string fileName = "export");
        //public interface IExportService<T>
        //{
        //Task<ExportResult> ExportDataAsync(IQueryable<T> query, ExportOptions options);
        //}
    }

    public class ExportResult
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
    //public class ExportOptions
    //{
    //    public string FileName { get; set; } = "export";
    //    public ExportFormat Format { get; set; } = ExportFormat.CSV;
    //    public List<ColumnMapping> ColumnMappings { get; set; } = new();
    //    public bool IncludeHeader { get; set; } = true;
    //}

    //public class ColumnMapping
    //{
    //    public string PropertyName { get; set; }
    //    public string DisplayName { get; set; }
    //}

    //public enum ExportFormat
    //{
    //    CSV,
    //    Excel
    //}

    //public class ExportResult
    //{
    //    public byte[] Content { get; set; }
    //    public string ContentType { get; set; }
    //    public string FileName { get; set; }
    //}
}

