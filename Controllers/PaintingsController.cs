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
            var paintings = await _context.Paintings.Include(p => p.Artist).ToListAsync();
            return View(paintings);
        }

        // GET: Paintings/Details/?id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings.Include(p => p.Artist).Include(p => p.Museum)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // GET: Paintings/Create
        public async Task<IActionResult> Create()
        {
            var artists = await _context.Artists.ToListAsync();
            var museums = await _context.Museums.ToListAsync();

            var paintingVM = new PaintingViewModel
            {
                Artists = artists,
                Museums = museums,

            };

            return View(paintingVM);
        }

        // POST: Paintings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageUrl,Title,Year,Medium, SelectedArtistId, SelectedMuseumId")] PaintingViewModel painting)
        {
            if (ModelState.IsValid)
            {
                var p = new Painting
                {
                    ImageUrl = painting.ImageUrl,
                    Title = painting.Title,
                    Year = painting.Year,
                    Medium = painting.Medium
                };
                p.Artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == painting.SelectedArtistId);
                p.Museum = await _context.Museums.FirstOrDefaultAsync(m => m.Id == painting.SelectedMuseumId);
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(painting);
        }

        // GET: Paintings/Edit/?id
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

            var artists = await _context.Artists.ToListAsync();
            var museums = await _context.Museums.ToListAsync();

            var paintingVM = new PaintingViewModel
            {
                Id = painting.Id,
                ImageUrl = painting.ImageUrl,
                Title = painting.Title,
                Year = painting.Year,
                Medium = painting.Medium,
                Artists = artists,
                Museums = museums,
                SelectedArtistId = painting.Artist.Id,
                SelectedMuseumId = painting.Museum.Id
            };

            return View(paintingVM);
        }

        // POST: Paintings/Edit/?id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ImageUrl,Title,Year,Medium, SelectedArtistId, SelectedMuseumId")] PaintingViewModel painting)
        {
            if (id != painting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var p = new Painting
                    {
                        Id = painting.Id,
                        ImageUrl = painting.ImageUrl,
                        Title = painting.Title,
                        Year = painting.Year,
                        Medium = painting.Medium
                    };
                    p.Artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == painting.SelectedArtistId);
                    p.Museum = await _context.Museums.FirstOrDefaultAsync(m => m.Id == painting.SelectedMuseumId);
                    _context.Update(p);
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

        // GET: Paintings/Delete/?id
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

        // POST: Paintings/Delete/?id
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
