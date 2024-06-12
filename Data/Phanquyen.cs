using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Phanquyen
{
    public int MaPq { get; set; }

    public string? MaPb { get; set; }

    public int? MaTrang { get; set; }

    public int? Them { get; set; }

    public int? Sua { get; set; }

    public int? Xoa { get; set; }

    public int? Xem { get; set; }

    public virtual Phongban? MaPbNavigation { get; set; }

    public virtual Trangweb? MaTrangNavigation { get; set; }
}
