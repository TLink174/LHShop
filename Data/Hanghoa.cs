using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Hanghoa
{
    public int MaHh { get; set; }

    public string TenHh { get; set; } = null!;

    public string? TenAlias { get; set; }

    public int MaLoai { get; set; }

    public string? MoTaDonVi { get; set; }

    public double? DonGia { get; set; }

    public string? Hinh { get; set; }

    public DateTime? NgaySx { get; set; }

    public decimal? GiamGia { get; set; }

    public int? SoLanXem { get; set; }

    public string? MoTa { get; set; }

    public string MaNcc { get; set; } = null!;

    public virtual ICollection<Banbe> Banbes { get; set; } = new List<Banbe>();

    public virtual ICollection<Chitiethd> Chitiethds { get; set; } = new List<Chitiethd>();

    public virtual Loai MaLoaiNavigation { get; set; } = null!;

    public virtual Nhacungcap MaNccNavigation { get; set; } = null!;

    public virtual ICollection<Yeuthich> Yeuthiches { get; set; } = new List<Yeuthich>();
}
