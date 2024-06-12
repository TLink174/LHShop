using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LHShop.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LHShop.Helpers;
using LHShop.ViewModels;

namespace LHShop.Controllers
{
    public class YeuthichController : Controller
    {
        private readonly Lhshop2024Context _context;

        public YeuthichController(Lhshop2024Context context)
        {
            _context = context;
        }

        // GET: Yeuthiches
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(c => c.Type == MySetting.Claim_CustomerID)?.Value;

            if (userId == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            int favoriteCount = _context.Yeuthiches
                                          .Where(y => y.MaKh == userId)
                                          .Count();
            var yeuthichs = await _context.Yeuthiches
                                          .Where(y => y.MaKh == userId)
                                          .ToListAsync();
            var hangHoaIds = yeuthichs.Select(y => y.MaHh).ToList();

            var hangHoas = await _context.Hanghoas
                                         .Include(h => h.MaLoaiNavigation)
                                         .Where(h => hangHoaIds.Contains(h.MaHh))
                                         .ToListAsync();
            var result = hangHoas.Select(h => new YeuThichVM
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                DonGia = (double)(h.DonGia ?? 0),
                Hinh = h.Hinh,
                FavoriteCount = favoriteCount
            });
            
            return View(result);
        }

        // GET: Yeuthiches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuthich = await _context.Yeuthiches
                .Include(y => y.MaHhNavigation)
                .Include(y => y.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.MaYt == id);
            if (yeuthich == null)
            {
                return NotFound();
            }

            return View(yeuthich);
        }

        // GET: Yeuthiches/Create
        public IActionResult Create()
        {
            ViewData["MaHh"] = new SelectList(_context.Hanghoas, "MaHh", "MaHh");
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh");
            return View();
        }

        // POST: Yeuthiches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaYt,MaHh,MaKh,NgayChon,MoTa")] Yeuthich yeuthich)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yeuthich);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHh"] = new SelectList(_context.Hanghoas, "MaHh", "MaHh", yeuthich.MaHh);
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", yeuthich.MaKh);
            return View(yeuthich);
        }

        // GET: Yeuthiches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuthich = await _context.Yeuthiches.FindAsync(id);
            if (yeuthich == null)
            {
                return NotFound();
            }
            ViewData["MaHh"] = new SelectList(_context.Hanghoas, "MaHh", "MaHh", yeuthich.MaHh);
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", yeuthich.MaKh);
            return View(yeuthich);
        }

        // POST: Yeuthiches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaYt,MaHh,MaKh,NgayChon,MoTa")] Yeuthich yeuthich)
        {
            if (id != yeuthich.MaYt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yeuthich);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YeuthichExists(yeuthich.MaYt))
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
            ViewData["MaHh"] = new SelectList(_context.Hanghoas, "MaHh", "MaHh", yeuthich.MaHh);
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", yeuthich.MaKh);
            return View(yeuthich);
        }

        // GET: Yeuthiches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yeuthich = await _context.Yeuthiches
                .Include(y => y.MaHhNavigation)
                .Include(y => y.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.MaYt == id);
            if (yeuthich == null)
            {
                return NotFound();
            }

            return View(yeuthich);
        }

        // POST: Yeuthiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yeuthich = await _context.Yeuthiches.FindAsync(id);
            if (yeuthich != null)
            {
                _context.Yeuthiches.Remove(yeuthich);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YeuthichExists(int id)
        {
            return _context.Yeuthiches.Any(e => e.MaYt == id);
        }

        [Authorize]
        public IActionResult AddToFavorites(int id)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(c => c.Type == MySetting.Claim_CustomerID)?.Value;

            if (userId == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }

            var favorite = _context.Yeuthiches.SingleOrDefault(f => f.MaKh == userId && f.MaHh == id);
            if (favorite == null)
            {
                var hangHoa = _context.Hanghoas.SingleOrDefault(h => h.MaHh == id);
                if (hangHoa == null)
                {
                    return Redirect("/error/404");
                }

                favorite = new Yeuthich
                {
                    MaHh = hangHoa.MaHh,
                    MaKh = userId,
                    NgayChon = DateTime.Now,
                };

                _context.Yeuthiches.Add(favorite);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
