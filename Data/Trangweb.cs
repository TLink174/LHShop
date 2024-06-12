using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Trangweb
{
    public int MaTrang { get; set; }

    public string TenTrang { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Phanquyen> Phanquyens { get; set; } = new List<Phanquyen>();
}
