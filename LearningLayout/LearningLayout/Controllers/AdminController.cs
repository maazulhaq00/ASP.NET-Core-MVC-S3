﻿using Microsoft.AspNetCore.Mvc;

namespace LearningLayout.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
    }
}
