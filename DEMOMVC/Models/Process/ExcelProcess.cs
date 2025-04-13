using System.Data;
using OfficeOpenXml;

namespace DemoMVC.Models.Process
{
    public class ExcelProcess
    {
        /// <summary>
        /// Reads data from an Excel file path and converts it to a DataTable.
        /// Compatible with .NET 9 and EPPlus 6+.
        /// </summary>
        /// <param name="strPath">The file path of the Excel file.</param>
        /// <returns>A DataTable containing the extracted data.</returns>
        public DataTable ExcelToDataTable(string strPath)
        {
            // Required for EPPlus to work
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var fileInfo = new FileInfo(strPath);
            using var package = new ExcelPackage(fileInfo);
            return ExcelToDataTable(package);
        }

        /// <summary>
        /// Reads data from an ExcelPackage and converts it to a DataTable.
        /// </summary>
        /// <param name="package">The ExcelPackage containing the workbook data.</param>
        /// <returns>A DataTable containing the extracted data.</returns>
        public DataTable ExcelToDataTable(ExcelPackage package)
        {
            // License setting still needed even in EPPlus 7+
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var dt = new DataTable();
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet?.Dimension == null)
                return dt;

            var columnNames = new List<string>();
            int currentColumn = 1;

            // Read header row
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();

                // Add default header if missing
                while (cell.Start.Column > currentColumn)
                {
                    string header = $"Header_{currentColumn}";
                    dt.Columns.Add(header);
                    columnNames.Add(header);
                    currentColumn++;
                }

                // Ensure uniqueness
                if (string.IsNullOrEmpty(columnName))
                {
                    columnName = $"Header_{currentColumn}";
                }

                int duplicates = columnNames.Count(x => x == columnName);
                if (duplicates > 0)
                {
                    columnName = $"{columnName}_{duplicates + 1}";
                }

                dt.Columns.Add(columnName);
                columnNames.Add(columnName);
                currentColumn++;
            }

            // Read data rows
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var newRow = dt.NewRow();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    newRow[col - 1] = worksheet.Cells[row, col].Text;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }
    }
}
