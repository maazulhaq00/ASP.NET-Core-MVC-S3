using EcommLogin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace EcommLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommContext _context;
        IWebHostEnvironment env;
        public HomeController(ILogger<HomeController> logger, EcommContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
           return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                HttpContext.Session.Remove("username");
            }
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserViewModel u1)
        {
            if (ModelState.IsValid) 
            {
                // 1. you store image in folder  
                string fileName = "";
                if(u1.UserImage != null)
                {
                    string folderPath = Path.Combine(env.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + u1.UserImage.FileName;

                    string fullPath = Path.Combine(folderPath, fileName);
                    u1.UserImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                // 2. Convert UserVM into User
                User u2 = new User
                {
                    UserName = u1.UserName,
                    UserEmail = u1.UserEmail,
                    FirstName = u1.FirstName,
                    LastName = u1.LastName,
                    UserPassword = u1.UserPassword,
                    UserImage = fileName,
                };
                // 3. Store User in DB
                _context.Users.Add(u2);
                _context.SaveChanges();
                TempData["register_success"] = "User Account Created";
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User formdata)
        {
            var user = _context.Users.Where(row => row.UserEmail == formdata.UserEmail && row.UserPassword == formdata.UserPassword).FirstOrDefault();

            if(user != null)
            {
                HttpContext.Session.SetString("username", user.UserName);
                HttpContext.Session.SetString("userimage", user.UserImage);
                return RedirectToAction("Index");
            }
            ViewBag.LoginFailed = "Incorrect user email or password";
            return View();
        }

        public IActionResult Display()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}