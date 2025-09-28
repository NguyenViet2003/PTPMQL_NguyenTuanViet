using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models
{
    public class Employee : Person
    {
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }
    }
}
