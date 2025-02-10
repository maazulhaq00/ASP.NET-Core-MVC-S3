using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace MyApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // Data from Route
        public IActionResult AboutUs(int? id, String name, int age)
        {
            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.Age = age;

            return View();
        }
        // Razor & Controller to View
        public IActionResult ContactUs()
        {
            ViewData["Students"] = new List<String>{ "Abbas", "Hammad", "Nausheen", "Umair", "Ayyan"};

            return View();
        }

        public IActionResult AppointmentForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AppointmentForm(String pname, String pemail, String disease)
        {

            ViewBag.Pname = pname;
            ViewBag.Pemail = pemail;
            ViewBag.Disease = disease;

            return View("AppointmentDetails");
        }
        public IActionResult DoctorForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoctorForm(Doctor d1)
        {
            return View("DoctorDetails", d1);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}