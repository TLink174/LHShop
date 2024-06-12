using LHShop.Areas.Admin.Models;
using LHShop.Data;
using LHShop.Helpers;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using X.PagedList;
using System.Linq;

namespace LHShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyHangHoaController : Controller
    {
        private readonly Lhshop2024Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UploadFile _uploadFile;
        private readonly ILogger<QuanLyHangHoaController> _logger;

        public QuanLyHangHoaController(Lhshop2024Context context, IWebHostEnvironment hostEnvironment, UploadFile uploadFile, ILogger<QuanLyHangHoaController> logger)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _uploadFile = uploadFile;
            _logger = logger;
        }

        private bool TenAliasExists(string tenAlias)
        {
            return _context.Hanghoas.Any(e => e.TenAlias == tenAlias);
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 10; // Số mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại

            var hangHoas = _context.Hanghoas.Select(h => new HangHoaMD
            {
                MaHh = h.MaHh,
                TenHh = h.TenHh,
                TenAlias = h.TenAlias,
                MaLoai = h.MaLoai,
                MoTaDonVi = h.MoTaDonVi,
                DonGia = h.DonGia,
                Hinh = h.Hinh,
                NgaySx = h.NgaySx,
                GiamGia = h.GiamGia,
                SoLanXem = h.SoLanXem,
                MoTa = h.MoTa,
                MaNcc = h.MaNcc
            }).ToPagedList(pageNumber, pageSize);

            return View(hangHoas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.Nhacungcaps, "MaNcc", "TenCongTy");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangHoaMD model)
        {
            if (ModelState.IsValid)
            {
                string tenAlias = model.TenHh?.GenerateAlias() ?? string.Empty;

                if (TenAliasExists(tenAlias))
                {
                    ModelState.AddModelError("TenAlias", "Tên alias này đã tồn tại. Vui lòng chọn tên khác.");
                    return View(model);
                }

                string uniqueFileName;

                if (model.HinhFile != null)
                {
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/Loai");
                    model.Hinh = uniqueFileName; // Lưu tên tệp vào thuộc tính Logo
                }

                var hangHoa = new Hanghoa
                {
                    TenHh = model.TenHh,
                    TenAlias = model.TenHh.GenerateAlias(),
                    MaLoai = model.MaLoai,
                    MoTaDonVi = model.MoTaDonVi,
                    DonGia = model.DonGia,
                    Hinh = model.Hinh,
                    NgaySx = model.NgaySx,
                    GiamGia = model.GiamGia,
                    SoLanXem = model.SoLanXem,
                    MoTa = model.MoTa,
                    MaNcc = model.MaNcc
                };

                _context.Database.BeginTransaction();
                try
                {
                    _context.Add(hangHoa);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    _context.Database.RollbackTransaction();
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            ViewBag.MaLoai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.Nhacungcaps, "MaNcc", "TenCongTy");
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var data = _context.Hanghoas.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            var hangHoa = new HangHoaMD
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                MaLoai = data.MaLoai,
                MoTaDonVi = data.MoTaDonVi,
                DonGia = data.DonGia,
                Hinh = data.Hinh,
                NgaySx = data.NgaySx,
                GiamGia = data.GiamGia,
                SoLanXem = data.SoLanXem,
                MoTa = data.MoTa,
                MaNcc = data.MaNcc
            };

            ViewBag.MaLoai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.Nhacungcaps, "MaNcc", "TenCongTy");
            return View(hangHoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, HangHoaMD model)
        {
            ViewBag.MaLoai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.Nhacungcaps, "MaNcc", "TenCongTy");
            if (ModelState.IsValid)
            {
                var data = _context.Hanghoas.Find(id);
                if (data == null)
                {
                    return NotFound();
                }

                string newTenAlias = model.TenHh?.GenerateAlias() ?? string.Empty;
                if (newTenAlias != data.TenAlias && TenAliasExists(newTenAlias))
                {
                    ModelState.AddModelError("TenAlias", "Tên alias này đã tồn tại. Vui lòng chọn tên khác.");
                    return View(model);
                }

                string uniqueFileName;

                if (model.HinhFile != null)
                {
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/Loai");
                    model.Hinh = uniqueFileName;
                }

                data.TenHh = model.TenHh;
                data.TenAlias = newTenAlias;
                data.MaLoai = model.MaLoai;
                data.MoTaDonVi = model.MoTaDonVi;
                data.DonGia = model.DonGia;
                data.NgaySx = model.NgaySx;
                data.GiamGia = model.GiamGia;
                data.SoLanXem = model.SoLanXem;
                data.MoTa = model.MoTa;
                data.MaNcc = model.MaNcc;
                data.Hinh = model.Hinh;

                _context.Database.BeginTransaction();
                try
                {
                    _context.Update(data);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    return RedirectToAction("Index");
                }
                catch (Exception ex) // Catch the specific exception and log the details
                {
                    _context.Database.RollbackTransaction();
                    ModelState.AddModelError(string.Empty, $"An error occurred while saving the data: {ex.Message}");
                    // Log the exception details (optional)
                    _logger.LogError(ex, "An error occurred while saving the data");
                }
            }
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var data = _context.Hanghoas.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Remove(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Xóa sản phẩm thất bại: {ex.Message}";
                return View("Error");
            }
        }
    }
}
