using LHShop.Data;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LHShop.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Lhshop2024Context _context;
        public MenuLoaiViewComponent(Lhshop2024Context context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(x => new MenuLoaiVM
            {
                MaLoai = x.MaLoai,
                TenLoai = x.TenLoai,
                SoLuong = x.Hanghoas.Count
            }).ToList();

            var compositeViewModel = new CompositeVM
            {
                MenuLoaiVMs = data
            };
            return View(compositeViewModel);
        }
    }
}
