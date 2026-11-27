using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _31241021966_BuiCongDanh.Models;
using _31241021966_BuiCongDanh.Data;
namespace _31241021966_BuiCongDanh.Controllers;
[Authorize] public class GenresController(ApplicationDbContext db):Controller {
    public async Task<IActionResult> Index()=>View(await db.Genres.OrderBy(x=>x.ID).ToListAsync());
    public async Task<IActionResult> Details(int id){var g=await db.Genres.Include(x=>x.Movies).FirstOrDefaultAsync(x=>x.ID==id);return g==null?NotFound():View(g);}
    public IActionResult Create()=>View(new Genre());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create([Bind("Name")]Genre genre){if(await db.Genres.AnyAsync(x=>x.Name==genre.Name))ModelState.AddModelError("Name","Thể loại đã tồn tại.");if(!ModelState.IsValid)return View(genre);db.Add(genre);await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var g=await db.Genres.FindAsync(id);return g==null?NotFound():View(g);}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,[Bind("ID,Name")]Genre genre){if(id!=genre.ID)return NotFound();var g=await db.Genres.FindAsync(id);if(g==null)return NotFound();if(await db.Genres.AnyAsync(x=>x.ID!=id&&x.Name==genre.Name))ModelState.AddModelError("Name","Thể loại đã tồn tại.");if(!ModelState.IsValid)return View(genre);g.Name=genre.Name;await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Delete(int id){var g=await db.Genres.FindAsync(id);return g==null?NotFound():View(g);}
    [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var g=await db.Genres.FindAsync(id);if(g==null)return NotFound();if(await db.Movies.AnyAsync(x=>x.GenreID==id)){TempData["Message"]="Không thể xóa thể loại đang có phim.";return RedirectToAction(nameof(Index));}db.Remove(g);await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
}
