using LHShop.Data;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHShop.Controllers
{
    public class HanghoaController : Controller
    {
        private readonly Lhshop2024Context _context;
        public HanghoaController(Lhshop2024Context context)
        {
            _context = context;
        }
        public IActionResult Index(int? loai)
        {
            var hangHoas = _context.Hanghoas.AsQueryable();

            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(h => h.MaLoai == loai.Value); // Filter by MaLoai in Hanghoas
            }
            var result = hangHoas.Select(h => new HanghoaVM
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                DonGia = (double)(h.DonGia ?? 0),
                Hinh = h.Hinh
            });

            var compositeVM = new CompositeVM
            {
                HangHoaVMs = result.ToList()
            };

            return View(compositeVM);
        }
        public IActionResult Search(string query)
        {
            var hangHoas = _context.Hanghoas.AsQueryable();

            if (query!= null)
            {
                hangHoas = hangHoas.Where(h => h.TenHh.Contains(query)); 
            }
            var result = hangHoas.Select(h => new HanghoaVM
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                DonGia = (double)(h.DonGia ?? 0),
                Hinh = h.Hinh
            });

            var compositeVM = new CompositeVM
            {
                HangHoaVMs = result.ToList()
            };
            return View(compositeVM);
        }
        public IActionResult Detail(int id)
        {
            var data = _context.Hanghoas
                .Include(h => h.MaLoaiNavigation)
                .SingleOrDefault(h => h.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
                return Redirect("/error/404");
            }
            var result = new ChiTietHanghoaVM
            {
                MaHH = data.MaHh,
                TenHH = data.TenHh,
                DonGia = (double)(data.DonGia ?? 0),
                Hinh = data.Hinh,
                ChiTiet = data.MoTa ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,
                DiemDanhGia = 5
            };
            return View(result);
        }
    }
}
