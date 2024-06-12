using LHShop.Data;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LHShop.ViewComponents
{
    public class HomeSanPhamViewComponent : ViewComponent
    {
        private readonly Lhshop2024Context _context;

        public HomeSanPhamViewComponent(Lhshop2024Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = new CompositeVM
            {
                HangHoaVMs = _context.Hanghoas
                .Select(x => new HanghoaVM
                {
                    MaHH = x.MaHh,
                    TenHH = x.TenHh,
                    DonGia = (double)x.DonGia,
                    Hinh = x.Hinh,
                })
                .Take(6)
                .ToList()
            };
            return View(data);
        }
    }
}
