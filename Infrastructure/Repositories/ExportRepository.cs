using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using ClosedXML.Excel;

namespace Infrastructure.Repositories
{
    public class ExportRepository : IExportRepository
    {
        public async Task<ExportResult> ExportToCsvAsync<T>(IEnumerable<T> data, string fileName = "export")
        {
            try
            {
                using var memoryStream = new MemoryStream();
                using (var streamWriter = new StreamWriter(memoryStream, leaveOpen: true))
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    await csvWriter.WriteRecordsAsync(data);
                    await streamWriter.FlushAsync();
                }

                return new ExportResult
                {
                    Content = memoryStream.ToArray(),
                    ContentType = "text/csv",
                    FileName = $"{fileName}_{DateTime.Now:yyyyMMdd}.csv"
                };
            }
            catch (Exception ex)
            {
                throw new ExportException("Failed to export data to CSV", ex);
            }
        }

        public async Task<ExportResult> ExportToExcelAsync<T>(IEnumerable<T> data, string sheetName = "Data", string fileName = "export")
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Insert data as a table starting from A1
                var tableRange = worksheet.Cell(1, 1).InsertTable(data);

                // Apply styling
                tableRange.Theme = XLTableTheme.TableStyleMedium9;

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                using var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);

                return new ExportResult
                {
                    Content = memoryStream.ToArray(),
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = $"{fileName}_{DateTime.Now:yyyyMMdd}.xlsx"
                };
            }
            catch (Exception ex)
            {
                throw new ExportException("Failed to export data to Excel", ex);
            }
        }
    }

    public class ExportException : Exception
    {
        public ExportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}