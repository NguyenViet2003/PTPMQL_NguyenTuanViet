using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ Tự động tăng ID
        public int PersonId { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
