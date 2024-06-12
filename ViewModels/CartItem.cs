namespace LHShop.ViewModels
{
    public class CartItem
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien => SoLuong * DonGia;
        public string Hinh { get; set; }
    }
}
