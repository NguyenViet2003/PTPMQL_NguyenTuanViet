using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSDL.Data;
using CSDL.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 
using CSDL.Models.Process; 
using System.Data;
using OfficeOpenXml; 

namespace CSDL.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess; 

        public PersonController(ApplicationDbContext context, ExcelProcess excelProcess) 
        {
            _context = context;
            _excelProcess = excelProcess;
        }


        public IActionResult Download()
        {
            var fileName = "PersonData.xlsx";

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells["A1"].Value = "PersonId";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";
                worksheet.Cells["D1"].Value = "PhoneNumber";

                var personList = _context.Person.ToList();

                worksheet.Cells["A2"].LoadFromCollection(personList);

                var stream = new MemoryStream(excelPackage.GetAsByteArray());

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}