using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Entities;

namespace AppView.Controllers
{
    public class GiaoDichTaiChinhsController : Controller
    {
        private readonly AppDbContext _context;

        public GiaoDichTaiChinhsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GiaoDichTaiChinhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GiaoDichTaiChinhs.ToListAsync());
        }

        // GET: GiaoDichTaiChinhs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaoDichTaiChinh = await _context.GiaoDichTaiChinhs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }

            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiaoDichTaiChinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NguoiThucHien,LoaiGiaoDich,DiaChiVi,SoLuong,NgayGiaoDich,PhiGiaoDich,TrangThai")] GiaoDichTaiChinh giaoDichTaiChinh)
        {
            if (ModelState.IsValid)
            {
                giaoDichTaiChinh.Id = Guid.NewGuid();
                _context.Add(giaoDichTaiChinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaoDichTaiChinh = await _context.GiaoDichTaiChinhs.FindAsync(id);
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }
            return View(giaoDichTaiChinh);
        }

        // POST: GiaoDichTaiChinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NguoiThucHien,LoaiGiaoDich,DiaChiVi,SoLuong,NgayGiaoDich,PhiGiaoDich,TrangThai")] GiaoDichTaiChinh giaoDichTaiChinh)
        {
            if (id != giaoDichTaiChinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaoDichTaiChinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaoDichTaiChinhExists(giaoDichTaiChinh.Id))
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
            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaoDichTaiChinh = await _context.GiaoDichTaiChinhs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }

            return View(giaoDichTaiChinh);
        }

        // POST: GiaoDichTaiChinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var giaoDichTaiChinh = await _context.GiaoDichTaiChinhs.FindAsync(id);
            if (giaoDichTaiChinh != null)
            {
                _context.GiaoDichTaiChinhs.Remove(giaoDichTaiChinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaoDichTaiChinhExists(Guid id)
        {
            return _context.GiaoDichTaiChinhs.Any(e => e.Id == id);
        }
    }
}
