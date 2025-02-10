using DbFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DbFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolMsContext context;

        public HomeController(ILogger<HomeController> logger, SchoolMsContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stdList = await context.Students.ToListAsync();
            return View(stdList);
        }

        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();

                TempData["insert_success"] = "Student Inserted Successfully";

                return RedirectToAction("Index");
            }
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> EditStudent(int? id)
        {
            if(id == null || context.Students == null)
            {
                return NotFound();
            }
            var student = await context.Students.FindAsync(id);

            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(int? id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Students.Update(student);
                await context.SaveChangesAsync();
                
                TempData["update_success"] = "Student Updated Successfully";

                return RedirectToAction("Index");
            }
            

            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id == null || context.Students == null)
            {
                return NotFound();
            }
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudentConfirmed(int? id)
        {
            if (id == null || context.Students == null)
            {
                return NotFound();
            }
            var student = await context.Students.FindAsync(id);
            if (student != null){
                context.Students.Remove(student);
                await context.SaveChangesAsync();

                TempData["delete_success"] = "Student Deleted Successfully";

            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}