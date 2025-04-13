using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;

namespace DemoMvc.Controllers
{
    public class GiatienController: Controller
    {
        public IActionResult Hoadon()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult Hoadon(Giatien model)
        {
            double Tongtien = model.soluong * model.dongia;
            ViewBag.tongtien ="Tổng tiền hóa đơn: " + Tongtien;
            
            return View();

        }
}
}