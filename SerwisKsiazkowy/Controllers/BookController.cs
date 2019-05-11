using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using SerwisKsiazkowy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using SerwisKsiazkowy.App_Start;

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
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == id).ToList();
            var reviews = db.Reviews.Include(p => p.User).Where(c => c.BookId == id).ToList();
            var newComment = new Comment();
            newComment.BookId = id;
            ViewBag.Title = BookId.Single().Title.ToString();

            double? rate = -1;
            var userId = User.Identity.GetUserId();
            try
            {
                rate = db.Ratings.Where(r => r.BookId == id).Average(a => a.Value);

            }
            catch
            {
                rate = -1;
            }
            

            var userRate = db.Ratings.Where(r => r.BookId == id && r.UserId == userId).ToList();
            ViewBag.user = userId;

            //var vm = new HomeViewModel()
            var vm = new DetailsViewModels()
            {
                NewComment = newComment,
                Genres = genres,
                SelectedBook = BookId,
                Comments = comments,
                Ratings = rate,
                UserRate = userRate,
                Reviews = reviews

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
            var author = db.Books.Select(p => p.Author).Distinct();
            var authors = db.Books.ToList();
            var VM = new HomeViewModel
            {
                Authors = authors,
                Author = author.ToList(),
                Genres = genres
            };

            return PartialView("_GenresMenu", VM);
        }

        public ActionResult FilterList(HomeViewModel author, string UrlPath)
        {
            var genres = db.Genres.ToList();
            //var author = db.Books.Select(p => p.Author).Distinct();
            //string[] temp = null;

            List<String> temp = new List<string>();

            //int genre_start = UrlPath.LastIndexOf("/") + 1;
            //string genreName = UrlPath.Split('/').Last();
            string genreName = UrlPath.Split('/').ElementAt(2);

           
            if (author.Author1 != null)
            {      
                foreach(var item in author.Author1)
                {            
                     temp.Add(item);                                      
                }
            }
            IEnumerable<Book> selectedBook = null;
            //temp = "Adam Mickiewicz" + "," + "Homer";
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genreName.ToUpper()).Single();
            
            if(genre != null)
            {
                
                //selectedBook = genre.Books.Where(a => a.Author == temp && temp.Contains(a.Author));
                selectedBook = genre.Books.Where(a =>  temp.Contains(a.Author));
                //selectedBook = db.Books.SqlQuery("select * from books where author in (" + temp + ")").ToList();
            }

            //var VM = new HomeViewModel
            //{

            //    //Author = author.ToList(),
            //    SelectedBook = selectedBook,
            //    Genres = genres
            //};
            
            //var data = db.Books.SqlQuery("select * from books where author in ("+temp+")").ToList();
            ViewBag.selectedBooks = "selectedBook: "+temp+" "+ genreName+"data: ";
            return View("ListGenres",selectedBook);
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