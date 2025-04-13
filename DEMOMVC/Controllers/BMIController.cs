using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMvc.Models;

namespace DemoMvc.Controllers
{
    public class BMIController: Controller
    {
        public IActionResult index()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult index(BMI model)
        {
            double chisoBMI = model.cannang / (model.chieucao * model.chieucao);
            string message;
            if (chisoBMI < 18.5)
                message = "Bạn đang thiếu cân.";
            else if (chisoBMI >= 18.5 && chisoBMI < 24.9)
                message = "Bạn có cân nặng bình thường.";
            else if (chisoBMI >= 25 && chisoBMI < 29.9)
                message = "Bạn đang thừa cân.";
            else
                message = "Bạn đang béo phì.";
            
            ViewBag.Message = "Chi so BMI = " + chisoBMI + " Ban dang: " + message;
        
            return View();

        }
}
}