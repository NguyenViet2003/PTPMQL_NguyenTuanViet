using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Bmi
    {
        [Required(ErrorMessage = "Vui lòng nhập cân nặng.")]
        [Range(1, 300, ErrorMessage = "Cân nặng phải lớn hơn 0.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chiều cao.")]
        [Range(0.1, 3.0, ErrorMessage = "Chiều cao phải lớn hơn 0.")]
        public double Height { get; set; }

        public double BmiValue { get; set; }

        public string Result { get; set; }
    }
}