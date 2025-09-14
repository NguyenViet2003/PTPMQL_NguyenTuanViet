using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }  

        [Required]
        public string FullName { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
