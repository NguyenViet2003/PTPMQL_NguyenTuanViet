using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class BmiController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Trả về một đối tượng Bmi rỗng ban đầu
            return View(new Bmi());
        }

        [HttpPost]
        public IActionResult Index(Bmi model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra chiều cao > 0 để tránh chia cho 0
                if (model.Height > 0)
                {
                    // Công thức tính BMI: BMI = Cân nặng / (Chiều cao * Chiều cao)
                    model.BmiValue = model.Weight / (model.Height * model.Height);

                    // Đánh giá kết quả
                    if (model.BmiValue < 18.5)
                    {
                        model.Result = "Gầy";
                    }
                    else if (model.BmiValue < 25)
                    {
                        model.Result = "Bình thường";
                    }
                    else if (model.BmiValue < 30)
                    {
                        model.Result = "Thừa cân";
                    }
                    else
                    {
                        model.Result = "Béo phì";
                    }
                }
                else
                {
                    ModelState.AddModelError("Height", "Chiều cao phải lớn hơn 0.");
                }
            }

            
            return View(model);
        }
    }
}