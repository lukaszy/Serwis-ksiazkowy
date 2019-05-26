using Microsoft.AspNet.Identity;
using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SerwisKsiazkowy.Controllers
{
    public class HomeController : Controller
    {
        private BookContext db = new BookContext();

       

        //public ActionResult Index()
        //{
        //    //Genre newGenre = new Genre { Name = "Biografie" };
        //    //db.Genres.Add(newGenre);
        //    //db.SaveChanges();

        //    // var booksList = db.Books.ToList();

        //    var genres = db.Genres.ToList();

        //    var lastBooks = db.Books.OrderByDescending(a => a.Title).ToList();

        //    var vm = new HomeViewModel()
        //    {
        //        Genres = genres,
        //        LastBooks = lastBooks

        //    };
                
        //    return View(vm);
        //}

        public ActionResult Index(string searchString, int? page)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var books = from m in db.Books.OrderBy(d=>d.Title)
                        select m;
            var genres = db.Genres.ToList();
            //var authors = from m in db.Books select m.Author;
            var authors = db.Books.ToList().OrderByDescending(d => d.Title);

            double? rate = -1;
            var userId = User.Identity.GetUserId();
            try
            {
                rate = db.Ratings.Average(a => a.Value);

            }
            catch
            {
                rate = -1;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)).OrderByDescending(d => d.Title);
            }
            var bookVM = new HomeViewModel
            {
                Authors = authors.ToPagedList(pageNumber, pageSize),
                Genres = genres,
                LastBooks =  books.ToPagedList(pageNumber, pageSize)
                //Ratings = rate
               
                
            };
           
            return View(bookVM);
        }

        public ActionResult StaticContent(string viewname)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            return View(viewname);
        }
        [ChildActionOnly]
        public ActionResult GetRate(int id)
        {
            //double? rate = -1;
            string rate = "";
            double rateDouble;
            var userId = User.Identity.GetUserId();
            try
            {
                //rate = db.Ratings.Where(r => r.BookId == id).Average(a => a.Value).ToString();
                rateDouble = Math.Round(db.Ratings.Where(r => r.BookId == id).Average(a => a.Value),2);
                rate = rateDouble.ToString();

            }
            catch
            {
                rate = "Brak";
            }
            ViewBag.Value = rate;
            return PartialView();
        }

        public ActionResult About()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}