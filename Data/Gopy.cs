using System;
using System.Collections.Generic;

namespace LHShop.Data;

public partial class Gopy
{
    public string MaGy { get; set; } = null!;

    public int MaCd { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayGy { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public int? CanTraLoi { get; set; }

    public string? TraLoi { get; set; }

    public DateOnly? NgayTl { get; set; }

    public virtual Chude MaCdNavigation { get; set; } = null!;
}
