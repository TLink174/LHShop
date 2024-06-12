using LHShop.Data;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LHShop.ViewComponents
{
    public class LoaiLayoutViewComponent : ViewComponent
    {
        private readonly Lhshop2024Context _context;

        public LoaiLayoutViewComponent(Lhshop2024Context context) 
        { 
            _context = context;
        }

/*        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(x => new MenuLoaiVM
            {
                MaLoai = x.MaLoai,
                TenLoai = x.TenLoai,
                SoLuong = x.Hanghoas.Count
            }).ToList();
            return View(data);
        }*/

        public IViewComponentResult Invoke()
        {
            var data = new CompositeVM
            {
                MenuLoaiVMs = _context.Loais.Select(x => new MenuLoaiVM
                {
                    MaLoai = x.MaLoai,
                    TenLoai = x.TenLoai,
                    SoLuong = x.Hanghoas.Count
                }).ToList(),

                HangHoaVMs = _context.Hanghoas.Select(x => new HanghoaVM
                {
                    MaHH = x.MaHh,
                    TenHH = x.TenHh,
                    DonGia = (double)x.DonGia,
                    Hinh = x.Hinh,
                }).ToList()
            };
            return View(data);
        }
    }
}
