using EcommerceWebApp.Models;
using EcommerceWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EcommerceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommContext db;
        private readonly IEmailSender emailSender;
        IWebHostEnvironment env;
        public HomeController(ILogger<HomeController> logger, EcommContext db, IWebHostEnvironment env, IEmailSender emailSender)
        {
            _logger = logger;
            this.db = db;
            this.env = env;
            this.emailSender = emailSender;
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

                var cartItems = db.tbl_cartitem.Where(row => row.cust_id == cid && row.order_id == null).Include(item => item.product).ToList();

                return View(cartItems);
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult AddToCart(int qty, int pid)
        {
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                var cid = int.Parse(HttpContext.Session.GetString("customeruserid"));

                var cartItem = db.tbl_cartitem.Where(row => row.cust_id == cid && row.prod_id == pid && row.order_id == null).FirstOrDefault(); // null

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
                        order_id = null
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
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                int cid = int.Parse(HttpContext.Session.GetString("customeruserid"));

                ViewBag.Customer = db.tbl_customer.Find(cid);
                ViewBag.CartItems = db.tbl_cartitem.Where(row => row.cust_id == cid && row.order_id == null).Include(item => item.product).ToList();

                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order o1)
        {
            if (HttpContext.Session.GetString("customeruserid") != null)
            {
                if(ModelState.IsValid)
                {
                    db.tbl_order.Add(o1);
                    db.SaveChanges();

                    var cartItems = db.tbl_cartitem.Where(row => row.cust_id == o1.cust_id && row.order_id == null).ToList();

                    foreach(var item in cartItems)
                    {
                        item.order_id = o1.order_id;

                        db.tbl_cartitem.Update(item);
                    }

                    db.SaveChanges(true);

                    // SEND ORDER EMAILS

                    var cust = db.tbl_customer.Find(o1.cust_id);
                    var subject = "ORDER CONFIRMED";
                    var body = $"Dear {cust.customer_name}, Your Order has been placed successfully. Thankyou for shopping with us.";

                    await emailSender.SendEmailAsync(cust.customer_email, subject, body);

                    return RedirectToAction("Index");
                }

                return View("CheckOut");
            }
            return RedirectToAction("Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}