using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Hoadon
{
    public int MaHd { get; set; }

    public string MaKh { get; set; } = null!;

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayCan { get; set; }

    public DateTime? NgayGiao { get; set; }

    public string? HoTen { get; set; }

    public string DiaChi { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? CachThanhToan { get; set; }

    public string? CachVanChuyen { get; set; }

    public decimal? PhiVanChuyen { get; set; }

    public int? MaTrangThai { get; set; }

    public string? MaNv { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<Chitiethd> Chitiethds { get; set; } = new List<Chitiethd>();

    public virtual Khachhang MaKhNavigation { get; set; } = null!;

    public virtual Nhanvien? MaNvNavigation { get; set; }

    public virtual Trangthai? MaTrangThaiNavigation { get; set; }
}
