using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHShop.Data;

public partial class Loai
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public string? TenLoaiAlias { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<Hanghoa> Hanghoas { get; set; } = new List<Hanghoa>();

    [NotMapped]
    public IFormFile HinhFile { get; set; } // Tệp tải lên
}
