namespace BMI.Models
{
    public class BMIModel
    {
        public float Weight { get; set; } // Cân nặng (kg)
        public float Height { get; set; } // Chiều cao (m)
        public float BMI { get; set; } 

        public string GetBMICategory()
        {
            if (BMI < 18.5) return "Dưới cân";
            if (BMI < 24.9) return "Bình thường";
            if (BMI < 29.9) return "Thừa cân";
            return "Béo phì";
        }
    }
}
