using System;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace CSDL.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; 

                    if (worksheet == null)
                    {
                        return null; 
                    }

                    foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns])
                    {
                        dataTable.Columns.Add(firstRowCell.Text);
                    }

         
                    for (int rowNumber = 2; rowNumber <= worksheet.Dimension.Rows; rowNumber++)
                    {
                        var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.Columns];
                        DataRow newRow = dataTable.NewRow();

                        foreach (var cell in row)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text;
                        }

                        dataTable.Rows.Add(newRow);
                    }
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel file: {ex.Message}");
                return null;
            }
        }
    }
}