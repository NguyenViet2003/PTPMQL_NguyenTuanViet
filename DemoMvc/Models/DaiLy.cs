using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMvc.Models
{
    public class DaiLy
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string MaDaiLy { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDaiLy { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string NguoiDaiDien { get; set; }

        [StringLength(15)]
        public string DienThoai { get; set; }

        // -------- Khóa ngoại đến HeThongPhanPhoi --------
        [Required]
        [ForeignKey("HeThongPhanPhoi")]
        public string MaHTPP { get; set; }

        // Thuộc tính điều hướng (navigation property)
        public HeThongPhanPhoi HeThongPhanPhoi { get; set; }
    }
}
