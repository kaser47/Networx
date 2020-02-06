using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using VideoGamesRepository.Data;
using VideoGamesRepository.Data.Models;

namespace VideoGamesRepository.Controllers
{
    public class VideoGamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoGames
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoGames.ToListAsync());
        }

        // GET: VideoGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null)
            {
                return NotFound();
            }

            return View(videoGame);
        }

        // GET: VideoGames/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,PublishedYear,Price")] VideoGame videoGame)
        {
            if (ModelState.IsValid && Validate(videoGame))
            {
                _context.Add(videoGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(videoGame);
        }

        // GET: VideoGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames.FindAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }
            return View(videoGame);
        }

        // POST: VideoGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,PublishedYear,Price")] VideoGame videoGame)
        {
            Validate(videoGame);
            if (id != videoGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoGameExists(videoGame.Id))
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
            return View(videoGame);
        }

        // GET: VideoGames/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGame = await _context.VideoGames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null)
            {
                return NotFound();
            }

            return View(videoGame);
        }

        // POST: VideoGames/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);
            _context.VideoGames.Remove(videoGame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoGameExists(int id)
        {
            return _context.VideoGames.Any(e => e.Id == id);
        }

        private bool Validate(VideoGame videoGame)
        {
            bool isValid = true;
            ViewBag.ValidationMessages = new List<string>();

            if (_context.VideoGames.Any(e => e.Title == videoGame.Title))
            {
                ViewBag.ValidationMessages.Add($"Unable to add video game '{videoGame.Title}' it already exists.");
                isValid = false;
            }

            if (videoGame.PublishedYear < 1958)
            {
                ViewBag.ValidationMessages.Add($"Published year has to be greater than 1957, the first ever video game was released in 1958.");
                isValid = false;
            }

            if (videoGame.PublishedYear > DateTime.Now.Year)
            {
                ViewBag.ValidationMessages.Add($"Published year cannot be in the future");
                isValid = false;
            }

            return isValid;
        }
    }
}
