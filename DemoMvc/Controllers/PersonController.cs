using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;
using DemoMVC.Models;

namespace DemoMvc.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Person ps)
        {
            string strOutput = "Xin chao " + ps.PersonId + " - " + ps.FullName + " - " + ps.Address;
            ViewBag.InfoPerson = strOutput;
            return View();
        }
    }
}