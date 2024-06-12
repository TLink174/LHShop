namespace LHShop.ViewModels
{
    public class HanghoaVM
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string Mota { get; set; }
        public int MaLoai { get; set; }
    }
    public class ChiTietHanghoaVM
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
        public string ChiTiet { get; set; }
        public string TenLoai { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
}
