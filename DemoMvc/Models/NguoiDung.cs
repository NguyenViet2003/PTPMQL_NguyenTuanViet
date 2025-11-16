using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models
{
    public class NguoiDung
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập năm sinh.")]
        [Range(1900, 2100, ErrorMessage = "Năm sinh phải từ 1900 đến 2100.")]
        public int NamSinh { get; set; }

        public int TinhTuoi()
        {
            return DateTime.Now.Year - NamSinh;
        }
    }
}