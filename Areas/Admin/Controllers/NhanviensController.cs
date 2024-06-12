using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LHShop.Data;

namespace LHShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanviensController : Controller
    {
        private readonly Lhshop2024Context _context;

        public NhanviensController(Lhshop2024Context context)
        {
            _context = context;
        }

        // GET: Admin/Nhanviens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhanviens.ToListAsync());
        }

        // GET: Admin/Nhanviens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Admin/Nhanviens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,HoTen,Email,MatKhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        // GET: Admin/Nhanviens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        // POST: Admin/Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNv,HoTen,Email,MatKhau")] Nhanvien nhanvien)
        {
            if (id != nhanvien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.MaNv))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        // GET: Admin/Nhanviens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Admin/Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(string id)
        {
            return _context.Nhanviens.Any(e => e.MaNv == id);
        }
    }
}
