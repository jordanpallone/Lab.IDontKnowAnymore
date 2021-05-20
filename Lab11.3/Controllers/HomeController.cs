using Lab11._3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;

namespace Lab11._3.Controllers
{
    public class HomeController : Controller
    {

        static MySqlConnection db = new MySqlConnection("Server=localhost;Database=coffeecult;Uid=root;Password=abc123");

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterResponse(string firstname, string lastname, string email, string pnum, string password, string confirmpassword)
        {
            if (password != confirmpassword)
            {
                return RedirectToAction("Register");
            }

            ViewData["FN"] = firstname;
            ViewData["LN"] = lastname;
            ViewData["EM"] = email;
            ViewData["PN"] = pnum;
            return View();
        }

        public IActionResult Menu()
        {
            List<Product> prod = db.GetAll<Product>().ToList();
            return View(prod);
        }

        [HttpGet]
        public IActionResult detail(int id)
        {
            Product prod = db.Get<Product>(id);
            return View(prod);
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
