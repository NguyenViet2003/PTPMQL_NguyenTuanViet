using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;

namespace DemoMvc.Controllers
{
    public class SCOREController: Controller
    {
        public IActionResult index()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult index(SCORE model)
        {
            double TB = model.A * 0.6 + model.B * 0.3 + model.C * 0.1;
            ViewBag.Message = "Diem TB = " + TB;
            return View();

        }
}
}