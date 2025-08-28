using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Calculation());
        }

        [HttpPost]
        public IActionResult Index(Calculation model)
        {
            switch (model.Operation)
            {
                case "add":
                    model.Result = model.Number1 + model.Number2;
                    break;
                case "subtract":
                    model.Result = model.Number1 - model.Number2;
                    break;
                case "multiply":
                    model.Result = model.Number1 * model.Number2;
                    break;
                case "divide":
                    // Xử lý trường hợp chia cho 0
                    if (model.Number2 != 0)
                    {
                        model.Result = model.Number1 / model.Number2;
                    }
                    else
                    {
                        ModelState.AddModelError("Number2", "Không thể chia cho 0.");
                    }
                    break;
            }
            
            return View(model);
        }
    }
}