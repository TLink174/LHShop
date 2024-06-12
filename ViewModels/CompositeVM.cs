namespace LHShop.ViewModels
{
    public class CompositeVM
    {
        public IEnumerable<MenuLoaiVM> MenuLoaiVMs { get; set; }
        public IEnumerable<HanghoaVM> HangHoaVMs { get; set; }
    }
}
