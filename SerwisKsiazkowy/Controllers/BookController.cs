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

namespace SerwisKsiazkowy.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();
        // GET: Book
        


        public ActionResult Details(int id)
        {
            var BookId = db.Books.Where(g => g.BookId == id);
            //var BookTitle = db.Books.Where(g => g.Title.Replace(" ", "-").ToLower() == title);
            var genres = db.Genres.ToList();
            var books = BookId.ToList();
            var comments = db.Comments.Include("Book").Where(c => c.BookId == id).ToList();
            ViewBag.Title = BookId.Single().Title.ToString();
            var vm = new HomeViewModel()
            {
                Genres = genres,
                SelectedBook = BookId,
                Comments = comments
                //SelectedBook = BookTitle

            };
            return View(vm);
        }

        public ActionResult ListGenres(string genrename)
        {
           
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var books = genre.Books.ToList();
            ViewBag.Title = genre.Name.ToString();
            //var books = db.Books.OrderByDescending(b => b.Title).ToList();

            return View(books);
        }
        [ChildActionOnly]
        public ActionResult GenresMenu(string genrename)
        {
            var genres = db.Genres.ToList();

            return PartialView("_GenresMenu", genres);
        }

        public async Task<ActionResult> Index(string searchString, string genrename)
        {
            var books = from m in db.Books
                        select m;
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            var bookVM = new HomeViewModel
            {
               
                LastBooks = await books.ToListAsync()
            };

            return View(books);
        }
    }

    
}