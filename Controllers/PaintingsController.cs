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
    public class PaintingsController : Controller
    {
        private readonly MvcPaintingContext _context;

        public PaintingsController(MvcPaintingContext context)
        {
            _context = context;
        }

        // GET: Paintings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paintings.ToListAsync());
        }

        // GET: Paintings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // GET: Paintings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,Medium")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(painting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,Medium")] Painting painting)
        {
            if (id != painting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(painting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaintingExists(painting.Id))
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
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintingExists(int id)
        {
            return _context.Paintings.Any(e => e.Id == id);
        }
    }
}
