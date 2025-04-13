using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models;
using DemoMVC.Models.Process;
using Microsoft.AspNetCore.Http;

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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var person = await _context.Person.FindAsync(id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,Email")] Person person)
        {
            if (id != person.PersonId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
                return NotFound();

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "No file selected.");
                return View();
            }

            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ModelState.AddModelError("", "Please choose a valid Excel file (.xls or .xlsx).");
                return View();
            }

            try
            {
                var fileName = $"uploaded_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}{fileExtension}";

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var dt = _excelProcess.ExcelToDataTable(filePath);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(dt.Rows[i][0]?.ToString()))
                        continue;

                    var personId = dt.Rows[i][0].ToString();
                    if (_context.Person.Any(p => p.PersonId == personId))
                        continue; 

                    var person = new Person
                    {
                        PersonId = personId,
                        FullName = dt.Rows[i][1]?.ToString(),
                        Address = dt.Rows[i][2]?.ToString(),
                        Email = dt.Rows[i][3]?.ToString()
                    };

                    _context.Add(person);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"File upload failed: {ex.Message}");
                return View();
            }
        }
    }
}
