using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Phongban
{
    public string MaPb { get; set; } = null!;

    public string TenPb { get; set; } = null!;

    public string? ThongTin { get; set; }

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();

    public virtual ICollection<Phanquyen> Phanquyens { get; set; } = new List<Phanquyen>();
}
