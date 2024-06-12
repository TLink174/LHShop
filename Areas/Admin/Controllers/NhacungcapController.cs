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
using X.PagedList;

namespace LHShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhacungcapController : Controller
    {
        private readonly Lhshop2024Context _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UploadFile _uploadFile;

        public NhacungcapController(Lhshop2024Context context, IWebHostEnvironment hostEnvironment, UploadFile uploadFile)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _uploadFile = uploadFile; // Thêm biến này
        }


        // GET: Admin/Nhacungcap
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; // Số mục trên mỗi trang
            int pageNumber = page ?? 1; // Số trang hiện tại

            var nhacungcaps = await _context.Nhacungcaps.ToPagedListAsync(pageNumber, pageSize);
            return View(nhacungcaps);
        }

        // GET: Admin/Nhacungcap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // GET: Admin/Nhacungcap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nhacungcap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nhacungcap model)
        {
            
            if (ModelState.IsValid)
            {
                Console.WriteLine("Nhập được");
                string uniqueFileName = null;

                if (model.HinhFile != null)
                {
                    Console.WriteLine("Nhập hình");
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/NhaCC");
                    Console.WriteLine(uniqueFileName);
                    model.Logo = uniqueFileName; // Lưu tên tệp vào thuộc tính Logo
                }
                else
                {
                    Console.WriteLine("Không nhập hình");
                }    
                Console.WriteLine(uniqueFileName);

                var nhacungcap = new Nhacungcap
                {
                    TenCongTy = model.TenCongTy,
                    MaNcc = model.MaNcc,
                    DienThoai = model.DienThoai,
                    Email = model.Email,
                    DiaChi = model.DiaChi,
                    Logo = model.Logo,
                    MoTa = model.MoTa
                };

                _context.Database.BeginTransaction();
                try
                {
                    _context.Add(nhacungcap);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
            }

            // Log validation errors if ModelState is invalid
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }

            return View(model);
        }
        // GET: Admin/Nhacungcap/Edit/5
        public IActionResult Edit(string id)
        {
            var data = _context.Nhacungcaps
               .SingleOrDefault(h => h.MaNcc == id);
            var nhacungcap = new Nhacungcap
            {
                MaNcc = data.MaNcc,
                TenCongTy = data.TenCongTy,
                Logo = data.Logo,
                DienThoai = data.DienThoai,
                Email = data.Email,
                MoTa = data.MoTa,
                DiaChi = data.DiaChi,
            };
            return View(nhacungcap);
        }

        // POST: Admin/Nhacungcap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Nhacungcap model)
        {
            model.MaNcc = id;
            Console.WriteLine(model.MaNcc);
            if (ModelState.IsValid)
            {
                var data = _context.Nhacungcaps.SingleOrDefault(ncc => ncc.MaNcc == id);
                if (data == null)
                {
                    Console.WriteLine("Nhà cung cấp không tồn tại");
                    ModelState.AddModelError(string.Empty, "Nhà cung cấp không tồn tại.");
                    return View(model);
                }
                Console.WriteLine(data.TenCongTy);
                string uniqueFileName = null;

                if (model.HinhFile != null)
                {
                    uniqueFileName = _uploadFile.UploadFileIMG(model.HinhFile, "Hinh/NhaCC");
                    model.Logo = uniqueFileName; // Lưu tên tệp vào thuộc tính Logo
                }

                model.Logo = uniqueFileName;

                data.TenCongTy = model.TenCongTy;
                data.Logo = uniqueFileName;
                data.NguoiLienLac = model.NguoiLienLac;
                data.Email = model.Email;
                data.DienThoai = model.DienThoai;
                data.DiaChi = model.DiaChi;
                data.MoTa = model.MoTa;
                data.MaNcc = model.MaNcc;

                _context.Database.BeginTransaction();
                try
                {
                    _context.Update(data);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    Console.WriteLine($"An error occurred while saving the data: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Lỗi xảy ra khi lưu dữ liệu.");
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


        // GET: Admin/Nhacungcap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Admin/Nhacungcap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);
            if (nhacungcap != null)
            {
                _context.Nhacungcaps.Remove(nhacungcap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(string id)
        {
            return _context.Nhacungcaps.Any(e => e.MaNcc == id);
        }
    }
}
