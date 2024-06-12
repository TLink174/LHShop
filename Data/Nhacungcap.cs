using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHShop.Data;

public partial class Nhacungcap
{
    public string TenCongTy { get; set; } = null!;

    public string? Logo { get; set; } = null!;

    public string? NguoiLienLac { get; set; }

    public string Email { get; set; } = null!;

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? MoTa { get; set; }

    public string MaNcc { get; set; } = null!;

    [NotMapped]
    public IFormFile HinhFile { get; set; } // Tệp tải lên

    public virtual ICollection<Hanghoa> Hanghoas { get; set; } = new List<Hanghoa>();

   

}
