using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Nhanvien
{
    public string MaNv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? MatKhau { get; set; }

    public virtual ICollection<Chude> Chudes { get; set; } = new List<Chude>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual ICollection<Hoidap> Hoidaps { get; set; } = new List<Hoidap>();

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
