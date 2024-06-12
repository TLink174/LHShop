using LHShop.Data;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LHShop.ViewComponents
{
    public class HomeLoaiViewComponent : ViewComponent
    {
        private readonly Lhshop2024Context _context;
        public HomeLoaiViewComponent(Lhshop2024Context context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(x => new MenuLoaiVM
            {
                MaLoai = x.MaLoai,
                TenLoai = x.TenLoai,
                SoLuong = x.Hanghoas.Count,
                Hinh = x.Hinh
            }).ToList();

            var compositeViewModel = new CompositeVM
            {
                MenuLoaiVMs = data
            };
            return View("Menu", compositeViewModel);
        }
    }
}
