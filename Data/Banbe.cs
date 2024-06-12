using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Banbe
{
    public int MaBb { get; set; }

    public string? MaKh { get; set; }

    public int MaHh { get; set; }

    public string? HoTen { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? NgayGui { get; set; }

    public string? GhiChu { get; set; }

    public virtual Hanghoa MaHhNavigation { get; set; } = null!;

    public virtual Khachhang? MaKhNavigation { get; set; }
}
