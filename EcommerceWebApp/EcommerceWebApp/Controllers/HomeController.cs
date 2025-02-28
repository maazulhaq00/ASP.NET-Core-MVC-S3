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