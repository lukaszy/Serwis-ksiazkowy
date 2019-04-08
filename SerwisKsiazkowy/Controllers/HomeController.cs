using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisKsiazkowy.Controllers
{
    public class HomeController : Controller
    {
        private BookContext db = new BookContext();

        public ActionResult Index()
        {
            //Genre newGenre = new Genre { Name = "Powieść" };
            //db.Genres.Add(newGenre);
            //db.SaveChanges();

            var booksList = db.Books.ToList();
            return View();
        }
        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}