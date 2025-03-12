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
            var customer = db.tbl_customer.Where(row => row.customer_email == c1.customer_email && row.customer_password == c1.customer_password).FirstOrDefault();
            
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
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                var cid = int.Parse(HttpContext.Session.GetString("customeruserid"));

                var cartItems = db.tbl_cartitem.Where(row => row.cust_id == cid && row.order_id == 0).Include(item => item.product).ToList();

                return View(cartItems);
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(int qty, int pid)
        {
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                var cid = int.Parse(HttpContext.Session.GetString("customeruserid"));

                var cartItem = db.tbl_cartitem.Where(row => row.cust_id == cid && row.prod_id == pid).FirstOrDefault(); // null

                if(cartItem != null)
                {
                    cartItem.product_quantity = cartItem.product_quantity + qty;

                    db.tbl_cartitem.Update(cartItem);
                    db.SaveChanges();
                }
                else
                {
                    cartItem = new CartItem
                    {
                        prod_id = pid,
                        cust_id = cid,
                        product_quantity = qty,
                        order_id = 0
                    };

                    db.tbl_cartitem.Add(cartItem);
                    db.SaveChanges();
                }

                return RedirectToAction("ShoppingCart");
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