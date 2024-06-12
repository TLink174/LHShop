using System.ComponentModel.DataAnnotations;

namespace LHShop.ViewModels
{
    public class LoginVM
    {
        [Display (Name = "Tên đăng nhập")]
        [Required (ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [MaxLength (20, ErrorMessage = "Tên đăng nhập tối đa 20 ký tự")]
        public string UserName { get; set; }

        
        [Display (Name = "Mật khẩu")]
        [Required (ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}
