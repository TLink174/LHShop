using LHShop.Data;
using LHShop.Helpers;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHShop.Controllers
{
    public class CartController : Controller
    {
        private readonly Lhshop2024Context _context;
        public CartController(Lhshop2024Context context)
        {
            _context = context;
        }
        
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CartSessionKey) ?? new List<CartItem>();
            
        public IActionResult Index()
        { 
            return View(Cart);
        }
        [Authorize]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(m => m.MaHH == id);
            if (item == null)
            {
                var hangHoa = _context.Hanghoas
                    .Include(h => h.MaLoaiNavigation)
                    .SingleOrDefault(m => m.MaHh == id);
                if (hangHoa == null)
                {
                    return Redirect("/error/404");
                }
                item = new CartItem
                {
                    MaHH = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    Hinh = hangHoa.Hinh,
                    DonGia = (double)(hangHoa.DonGia ?? 0),
                    SoLuong = quantity

                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(MySetting.CartSessionKey, gioHang);

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult RemoveCart(int id) 
        { 
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(m => m.MaHH == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CartSessionKey, gioHang);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CartSessionKey) ?? new List<CartItem>();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CartSessionKey) ?? new List<CartItem>();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "HangHoa");
            }

            if (ModelState.IsValid)
            {
                var customerID = HttpContext.User.Claims.SingleOrDefault(c => c.Type == MySetting.Claim_CustomerID)?.Value;
                var khachHang = new Khachhang();
                if (model.GiongKhachHang)
                {
                    khachHang = _context.Khachhangs.SingleOrDefault(k => k.MaKh == customerID);
                }
                var hoaDon = new Hoadon
                {
                    MaKh = customerID,
                    HoTen = model.HoTen ?? khachHang?.HoTen,
                    DiaChi = model.DiaChi ?? khachHang?.DiaChi,
                    SoDienThoai = model.SoDienThoai ?? khachHang?.DienThoai,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "COD",
                    CachVanChuyen = "VietNamPost",
                    GhiChu = model.GhiChu
                };

                _context.Database.BeginTransaction();
                try
                {
                    _context.Add(hoaDon);
                    _context.SaveChanges();

                    var cthds = new List<Chitiethd>();
                    foreach (var item in cart)
                    {
                        cthds.Add(new Chitiethd
                        {
                            MaHd = hoaDon.MaHd,
                            SoLuong = item.SoLuong,
                            MaHh = item.MaHH,
                            DonGia = item.DonGia,
                            GiamGia = 0
                        });
                    }
                    _context.AddRange(cthds);
                    _context.SaveChanges();

                    _context.Database.CommitTransaction();

                    HttpContext.Session.Set<List<CartItem>>(MySetting.CartSessionKey, new List<CartItem>());

                    return View("Success");
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    ModelState.AddModelError(string.Empty, $"An error occurred while processing your order: {ex.Message}");
                }
            }

            return View(model);
        }

    }
}
