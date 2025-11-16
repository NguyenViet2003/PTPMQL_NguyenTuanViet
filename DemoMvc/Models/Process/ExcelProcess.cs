using System;
using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace DemoMvc.Models.Process
{
    public class ExcelProcess
    {
        /// <summary>
        /// Đọc file Excel và chuyển sang DataTable.
        /// Lưu ý: License của EPPlus phải được set 1 lần duy nhất trong Program.cs
        /// </summary>
        public DataTable ExcelToDataTable(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Đường dẫn file không hợp lệ.", nameof(path));

            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("Không tìm thấy file Excel: " + path);

            var dt = new DataTable();

            // Giả sử license đã được set trong Program.cs
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null || worksheet.Dimension == null)
                    return dt; // không có dữ liệu

                int startRow = worksheet.Dimension.Start.Row;
                int endRow = worksheet.Dimension.End.Row;
                int startCol = worksheet.Dimension.Start.Column;
                int endCol = worksheet.Dimension.End.Column;

                // Đọc tiêu đề (dòng đầu tiên)
                for (int col = startCol; col <= endCol; col++)
                {
                    string header = worksheet.Cells[startRow, col].Text?.Trim();
                    if (string.IsNullOrEmpty(header))
                        header = $"Column{col}";
                    if (dt.Columns.Contains(header))
                        header += $"_{col}";
                    dt.Columns.Add(header);
                }

                // Đọc dữ liệu
                for (int row = startRow + 1; row <= endRow; row++)
                {
                    var dataRow = dt.NewRow();
                    bool hasData = false;

                    for (int col = startCol; col <= endCol; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Text ?? string.Empty;
                        dataRow[col - startCol] = cellValue;
                        if (!string.IsNullOrEmpty(cellValue))
                            hasData = true;
                    }

                    if (hasData)
                        dt.Rows.Add(dataRow);
                }
            }

            return dt;
        }
    }
}
