using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class BirthdayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Result", new { hoTen = nguoiDung.HoTen, namSinh = nguoiDung.NamSinh });
            }

            return View(nguoiDung);
        }

        [HttpGet]
        public IActionResult Result(string hoTen, int namSinh)
        {
            var nguoiDung = new NguoiDung
            {
                HoTen = hoTen,
                NamSinh = namSinh
            };

            return View(nguoiDung);
        }
    }
}