using AutoMapper;
using LHShop.Data;
using LHShop.Helpers;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LHShop.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Lhshop2024Context _context;
        private readonly IMapper _mapper;

        public KhachHangController(Lhshop2024Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<Khachhang>(model);
                    khachHang.RandomKey = Util.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = Util.UploadHinh(Hinh, "KhachHang");
                    }

                    _context.Add(khachHang);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return View();
        }
        #endregion

        #region Login in

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        { 
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _context.Khachhangs.SingleOrDefault(kh =>
                        kh.DienThoai == model.UserName);
                    if (khachHang == null)
                    {
                        ModelState.AddModelError("", "Sai thông tin đăng nhập");
                    }
                    else
                    {
                        if (!khachHang.HieuLuc)
                        {
                            ModelState.AddModelError("", "Tài khoản bị khóa");
                        }
                        else
                        {
                            if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                            {
                                ModelState.AddModelError("", "Sai thông tin đăng nhập");
                            }
                            else
                            {
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email, khachHang.Email),
                                    new Claim(ClaimTypes.Name, khachHang.HoTen),
                                    new Claim(MySetting.Claim_CustomerID, khachHang.MaKh),
                                    new Claim(ClaimTypes.Role, "Customer"),

                                };

                                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                                await HttpContext.SignInAsync(claimsPrincipal);

                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return Redirect("/");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                
            }

            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
