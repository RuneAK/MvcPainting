using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcPainting.Data;
using MvcPainting.Models;

namespace MvcPainting.Controllers
{
    public class MuseumsController : Controller
    {
        private readonly MvcPaintingContext _context;

        public MuseumsController(MvcPaintingContext context)
        {
            _context = context;
        }

        // GET: Museums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Museums.ToListAsync());
        }

        // GET: Museums/Details/?id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var museum = await _context.Museums.Include(m => m.Paintings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (museum == null)
            {
                return NotFound();
            }

            return View(museum);
        }

        // GET: Museums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Museums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Director")] Museum museum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(museum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(museum);
        }

        // GET: Museums/Edit/?id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var museum = await _context.Museums.FindAsync(id);
            if (museum == null)
            {
                return NotFound();
            }
            return View(museum);
        }

        // POST: Museums/Edit/?id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Director")] Museum museum)
        {
            if (id != museum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(museum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuseumExists(museum.Id))
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
            return View(museum);
        }

        // GET: Museums/Delete/?id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var museum = await _context.Museums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (museum == null)
            {
                return NotFound();
            }

            return View(museum);
        }

        // POST: Museums/Delete/?id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var museum = await _context.Museums.FindAsync(id);
            var paintings = await _context.Paintings.Where(p => p.Museum == museum).ToListAsync();
            _context.Paintings.RemoveRange(paintings);
            _context.Museums.Remove(museum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuseumExists(int id)
        {
            return _context.Museums.Any(e => e.Id == id);
        }
    }
}
