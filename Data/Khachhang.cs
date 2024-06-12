using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHShop.Data;

public partial class Khachhang
{
    public string MaKh { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string HoTen { get; set; } = null!;

    public string? GioiTinh { get; set; }

    public DateTime NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string Email { get; set; } = null!;

    public string? Hinh { get; set; }

    public bool HieuLuc { get; set; } = true;

    public int? VaiTro { get; set; }

    public string? RandomKey { get; set; }

    public virtual ICollection<Banbe> Banbes { get; set; } = new List<Banbe>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual ICollection<Yeuthich> Yeuthiches { get; set; } = new List<Yeuthich>();
    [NotMapped]
    public IFormFile? HinhFile { get; set; }
    
}
