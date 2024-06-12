using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Phancong
{
    public int MaPc { get; set; }

    public string MaNv { get; set; } = null!;

    public string MaPb { get; set; } = null!;

    public DateTime? NgayPc { get; set; }

    public ulong? HieuLuc { get; set; }

    public virtual Nhanvien MaNvNavigation { get; set; } = null!;

    public virtual Phongban MaPbNavigation { get; set; } = null!;
}
