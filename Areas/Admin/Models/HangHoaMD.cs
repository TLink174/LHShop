using System.ComponentModel.DataAnnotations.Schema;

namespace LHShop.Areas.Admin.Models
{
    public class HangHoaMD
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

        [NotMapped]
        public IFormFile HinhFile { get; set; } // Tệp tải lên
    }
}
