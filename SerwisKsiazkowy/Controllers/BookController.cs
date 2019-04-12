using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisKsiazkowy.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var BookId = db.Books.Where(g => g.BookId == id);
            var genres = db.Genres.ToList();
            var books = BookId.ToList();

            var vm = new HomeViewModel()
            {
                Genres = genres,
                SelectedBook = BookId

            };
            return View(vm);
        }

        public ActionResult List(string genrename)
        {
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var books = genre.Books.ToList();
            //var books = db.Books.OrderByDescending(b => b.Title).ToList();

            return View(books);
        }
    }
}