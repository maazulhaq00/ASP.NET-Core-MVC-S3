using EcommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EcommerceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommContext db;
        IWebHostEnvironment env;
        public HomeController(ILogger<HomeController> logger, EcommContext db, IWebHostEnvironment env)
        {
            _logger = logger;
            this.db = db;
            this.env = env;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.tbl_customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customer c1)
        {
            var customer = db.tbl_customer.Where(row => 
            row.customer_email == c1.customer_email && 
            row.customer_password == c1.customer_password).FirstOrDefault();
            
            if(customer != null)
            {
                HttpContext.Session.SetString("customeruserid", customer.customer_id.ToString());
                HttpContext.Session.SetString("customer_name", customer.customer_name);

                return RedirectToAction("Index");

            }
            ViewBag.LoginError = "Incorrect Email or Password.";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("customeruserid");
            HttpContext.Session.Remove("customer_name");

            return RedirectToAction("Login");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Shop()
        {
            var products = db.tbl_product.Include(p=>p.category).ToList();
            return View(products);
        }
        public IActionResult ShopDetails()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ShoppingCart(int qty, int pid)
        {
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                // Add to Cart
                return View();
            }
            
            return RedirectToAction("Login");
        }
        public IActionResult CheckOut()
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