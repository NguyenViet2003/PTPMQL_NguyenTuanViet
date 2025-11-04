using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models
{
    public class HeThongPhanPhoi
    {
        [Key]  // Khóa chính
        [Required]
        [StringLength(20)]
        public string MaHTPP { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHTPP { get; set; }
    }
}
