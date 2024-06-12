using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Hoidap
{
    public int MaHd { get; set; }

    public string CauHoi { get; set; } = null!;

    public string TraLoi { get; set; } = null!;

    public DateTime? NgayDua { get; set; }

    public string MaNv { get; set; } = null!;

    public virtual Nhanvien MaNvNavigation { get; set; } = null!;
}
