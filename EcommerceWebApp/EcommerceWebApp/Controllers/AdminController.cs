using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EcommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly EcommContext db;
        public AdminController(EcommContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("adminuserid") != null)
            {
                return View();
            }

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin adminForm)
        {
            var adminData = db.tbl_admin.Where(row => row.admin_email == adminForm.admin_email && row.admin_password == adminForm.admin_password).SingleOrDefault();
            
            if(adminData != null) 
            {
                HttpContext.Session.SetString("adminuserid", adminData.admin_id.ToString());
                HttpContext.Session.SetString("adminusername", adminData.admin_name);
                HttpContext.Session.SetString("adminimage", adminData.admin_image);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Incorrect Email or Password";

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("adminuserid");
            HttpContext.Session.Remove("adminusername");
            HttpContext.Session.Remove("adminimage");

            return RedirectToAction("Login");
        }
    }
}
