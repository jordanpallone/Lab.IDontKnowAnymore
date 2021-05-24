using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Lab11._3.Models;

namespace Lab11._3.Controllers
{
    public class AdminController : Controller
    {
        static MySqlConnection db = new MySqlConnection("Server=localhost;Database=coffeecult;Uid=root;Password=abc123");

        public IActionResult Index()
        {
            List<Product> prods = db.GetAll<Product>().ToList();
            return View(prods);
        }

        public IActionResult Detail(int id)
        {
            Product prod = db.Get<Product>(id);
            return View(prod);
        }

        
        public IActionResult EditForm(int id)
        {
            Product prod = db.Get<Product>(id);
            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            db.Update(prod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product prod = db.Get<Product>(id);
            db.Delete(prod);
            return RedirectToAction("Index");
        }

        public IActionResult AddForm()
        {
            return View();
        }

        public IActionResult Add(Product prod)
        {
            db.Insert(prod);
            return RedirectToAction("Index");
        }
    }
}
