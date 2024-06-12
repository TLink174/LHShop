using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LHShop.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string MaKH { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string HoTen { get; set; } = null!;

        public string GioiTinh { get; set; } = "Nam";

        [Display(Name = "Ngày sinh")]
        
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string DiaChi { get; set; }

        [Display(Name = "Điện thoại")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 kí tự")]
        /*[RegularExpression(@"0[2-9]\d{8} ", ErrorMessage = "So dien thoai khong hop le")]*/
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hop le")]
        public string Email { get; set; } = null!;

        public string? Hinh { get; set; }
    }
}
