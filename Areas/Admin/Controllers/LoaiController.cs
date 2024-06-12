using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LHShop.Data;
using LHShop.Helpers;
using Microsoft.Extensions.Hosting;
using LHShop.Areas.Admin.Models;
using X.PagedList;

namespace LHShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        private readonly Lhshop2024Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UploadFile _uploadFile;
        private bool TenAliasExists(string tenLoaiAlias)
        {
            return _context.Loais.Any(e => e.TenLoaiAlias == tenLoaiAlias);
        }

        public LoaiController(Lhshop2024Context context, IWebHostEnvironment hostEnvironment, UploadFile uploadFile)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _uploadFile = uploadFile;
        }

        // GET: Admin/Loai
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; // Số mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại

            var loais = await _context.Loais.ToPagedListAsync(pageNumber, pageSize);
            return View(loais);
        }

        // GET: Admin/Loai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // GET: Admin/Loai/Create
        public IActionResult Create()
        {
            Console.WriteLine("Chào mọi người".GenerateAlias());
            return View();
        }

        // POST: Admin/Loai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loai model)
        {
            if (ModelState.IsValid)
            {
                // Kiem tra ten alias
                string tenAlias = model.TenLoai?.GenerateAlias() ?? string.Empty;

                // Kiểm tra nếu TenAlias đã tồn tại
                if (TenAliasExists(tenAlias))
                {
                    ModelState.AddModelError("TenLoaiAlias", "Tên alias này đã tồn tại. Vui lòng chọn tên khác.");
                    return View(model);
                }
                string uniqueFileName;

                if (model.HinhFile != null)
                {
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/Loai");
                    model.Hinh = uniqueFileName; // Lưu tên tệp vào thuộc tính Logo
                }

                var loai = new Loai
                {
                    TenLoai = model.TenLoai,
                    TenLoaiAlias = model.TenLoai.GenerateAlias() ?? string.Empty,
                    MoTa = model.MoTa,
                    Hinh = model.Hinh
                };
                _context.Database.BeginTransaction();
                try
                {
                    _context.Add(loai);
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
            return View(model);
        }

        // GET: Admin/Loai/Edit/5
        public IActionResult Edit(int id)
        {
            var data = _context.Loais
               .SingleOrDefault(h => h.MaLoai == id);
            var loai = new Loai
            {
                MaLoai = data.MaLoai,
                TenLoai = data.TenLoai,
                TenLoaiAlias = data.TenLoaiAlias,
                MoTa = data.MoTa,
                Hinh = data.Hinh
            };
            return View(loai);
        }
        // POST: Admin/Loai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Loai model)
        {
            if (ModelState.IsValid)
            {
                var loai = _context.Loais.Find(id);
                Console.WriteLine(loai.Hinh);
                if (loai == null)
                {
                    return NotFound();
                }

                // Kiem tra ten alias
                string newTenAlias = model.TenLoai?.GenerateAlias() ?? string.Empty;

                // Kiểm tra nếu TenAlias đã tồn tại và khác với alias hiện tại
                if (newTenAlias != loai.TenLoaiAlias && TenAliasExists(newTenAlias))
                {
                    ModelState.AddModelError("TenAlias", "Tên alias này đã tồn tại. Vui lòng chọn tên khác.");
                    return View(model);
                }

                string uniqueFileName = null;

                if (model.HinhFile != null)
                {
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/Loai");
                    model.Hinh = uniqueFileName; // Lưu tên tệp vào thuộc tính Logo
                }

                Console.WriteLine(model.MoTa);
                loai.TenLoai = model.TenLoai;

                // Chỉ cập nhật tên alias nếu tên loại thay đổi
                if (loai.TenLoai != model.TenLoai)
                {
                    loai.TenLoaiAlias = model.TenLoai.GenerateAlias() ?? string.Empty;
                }

                loai.MoTa = model.MoTa;
                loai.Hinh = model.Hinh;

                _context.Database.BeginTransaction();
                try
                {
                    _context.Update(loai);
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
            return View(model);
        }

        // GET: Admin/Loai/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var data = _context.Loais
                .SingleOrDefault(h => h.MaLoai == id);
            if (data == null)
            {
                // Nếu sản phẩm không tồn tại, chuyển hướng người dùng đến một trang hoặc trang index, hoặc làm bất kỳ điều gì phù hợp với ứng dụng của bạn.
                return RedirectToAction("Index");
            }

            try
            {
                _context.Remove(data);
                _context.SaveChanges();

                // Sau khi xóa thành công, chuyển hướng người dùng đến một trang hoặc trang index, hoặc làm bất kỳ điều gì phù hợp với ứng dụng của bạn.
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có

                // Trả về một trang hoặc làm bất kỳ điều gì phù hợp với ứng dụng của bạn.
                // Ví dụ: trả về một trang báo lỗi hoặc hiển thị một thông báo lỗi trên cùng một trang.
                ViewBag.ErrorMessage = $"Xóa loại thất bại: {ex.Message}";
                return View("Error");
            }
        }
    }
}
