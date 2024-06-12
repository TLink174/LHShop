using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Chude
{
    public int MaCd { get; set; }

    public string? TenCd { get; set; }

    public string? MaNv { get; set; }

    public virtual ICollection<Gopy> Gopies { get; set; } = new List<Gopy>();

    public virtual Nhanvien? MaNvNavigation { get; set; }
}
