using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext db;
        public AdminController(AppDbContext context)
        {
            this.db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewCategories()
        {
            var categories = await db.Categories.ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewCategories");
            }
            return View(category);

        }
        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if(id == null || db.Categories == null)
            {
                return NotFound();
            }
            var category = await db.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(int? id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewCategories");
            }

            return View(category);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null || db.Categories == null)
            {
                return NotFound();
            }
            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategoryConfirmed(int? id)
        {
            if (id == null || db.Categories == null)
            {
                return NotFound();
            }
            var category = await db.Categories.FindAsync(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("ViewCategories");

        }
    }
}
