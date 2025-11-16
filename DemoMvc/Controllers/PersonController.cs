using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models;
using DemoMvc.Models.Process;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.IO;

namespace DemoMvc.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Address,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // ============================
        // ✅ ACTION UPLOAD EXCEL
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn file Excel để tải lên!");
                return View();
            }

            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ModelState.AddModelError("", "Chỉ hỗ trợ file Excel (.xls, .xlsx)");
                return View();
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Excels");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            DataTable dt = _excelProcess.ExcelToDataTable(filePath);

            if (dt.Rows.Count == 0)
            {
                ModelState.AddModelError("", "File Excel không có dữ liệu!");
                return View();
            }

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    var person = new Person
                    {
                        FullName = row[0]?.ToString() ?? string.Empty,
                        Address = row.Table.Columns.Count > 1 ? row[1]?.ToString() : null,
                        Email = row.Table.Columns.Count > 2 ? row[2]?.ToString() : null
                    };

                    if (!string.IsNullOrWhiteSpace(person.FullName))
                    {
                        _context.Person.Add(person);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi đọc dòng Excel: {ex.Message}");
                    continue;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ============================
        // ✅ ACTION DOWNLOAD EXCEL
        // ============================
        public IActionResult Download()
        {
            var fileName = "PersonList.xlsx";

            using (var excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                // 🔹 Header
                worksheet.Cells["A1"].Value = "PersonID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";
                worksheet.Cells["D1"].Value = "Email";

                // 🔹 Lấy dữ liệu
                var personList = _context.Person.ToList();

                // 🔹 Đổ dữ liệu bắt đầu từ A2
                worksheet.Cells["A2"].LoadFromCollection(personList, false);

                // 🔹 Xuất file
                var stream = new MemoryStream(excelPackage.GetAsByteArray());

                return File(
                    stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
        }
    }
}
