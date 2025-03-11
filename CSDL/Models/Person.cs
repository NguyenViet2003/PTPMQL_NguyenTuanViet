using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDL.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public string PersonId { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!; // Thêm thuộc tính PhoneNumber
    }
}