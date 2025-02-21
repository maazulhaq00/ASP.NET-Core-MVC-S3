using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EcommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly EcommContext db;
        IWebHostEnvironment env;
        public AdminController(EcommContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
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
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("adminuserid") != null)
            {
                //Work

                int id = int.Parse(HttpContext.Session.GetString("adminuserid"));

                var admin = db.tbl_admin.Find(id);

                AdminViewModel admin1 = new AdminViewModel
                {
                    admin_id = admin.admin_id,
                    admin_name = admin.admin_name,
                    admin_email = admin.admin_email,
                    admin_password = admin.admin_password,
                };

                ViewBag.admin_image = admin.admin_image;
                
                return View(admin1);
            }

            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult Profile(AdminViewModel admin1)
        {
            int id = int.Parse(HttpContext.Session.GetString("adminuserid"));
            var admin = db.tbl_admin.Find(id);

            if (ModelState.IsValid)
            {
                string fileName = admin.admin_image;

                if(admin1.admin_image != null)
                {
                    string folderPath = Path.Combine(env.WebRootPath, "images","admin");
                    fileName = Guid.NewGuid().ToString() +"_"+  admin1.admin_image.FileName;
                    string fullPath = Path.Combine(folderPath, fileName);
                    admin1.admin_image.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                admin.admin_name = admin1.admin_name;
                admin.admin_email = admin1.admin_email;
                admin.admin_password = admin1.admin_password;
                admin.admin_image = fileName;
                
                HttpContext.Session.SetString("adminimage", fileName);

                db.tbl_admin.Update(admin);
                db.SaveChanges();
            }

            ViewBag.admin_image = admin.admin_image;

            return View();

        }
        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetString("adminuserid") != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult AddCategory(Category c1)
        {
            if (HttpContext.Session.GetString("adminuserid") != null)
            {
                if (ModelState.IsValid)
                {
                    db.tbl_category.Add(c1);
                    db.SaveChanges();
                    return RedirectToAction("ViewCategories");
                }
                return View();
            }
            return RedirectToAction("Login");
        }
        public IActionResult ViewCategories()
        {
            if (HttpContext.Session.GetString("adminuserid") != null)
            {
                var categories = db.tbl_category.ToList();
                return View(categories);
            }

            return RedirectToAction("Login");
        }
        public IActionResult AddProduct()
        {
            if (HttpContext.Session.GetString("adminuserid") != null)
            {
                var categoriesDB = db.tbl_category.ToList();
                
                var categoriesSL = new List<SelectListItem>();

                foreach (var category in categoriesDB)
                {
                    categoriesSL.Add(
                        new SelectListItem{
                            Text = category.category_name,
                            Value = category.category_id.ToString()
                        }
                    );
                }

                ViewBag.categoriesSL = categoriesSL;


                return View();
            }
            return RedirectToAction("Login");
        }
    }
}
