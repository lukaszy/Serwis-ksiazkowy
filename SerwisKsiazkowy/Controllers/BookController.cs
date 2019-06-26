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
using PagedList;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SerwisKsiazkowy.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();
        // GET: Book

        public PartialViewResult CommentPartial(int id)
        {
            var comments = db.Comments.Include(p => p.User).Include(p=>p.Book).Where(p=>p.BookId == id).OrderByDescending(d => d.DateAdded).Take(20).ToList();
            return PartialView("_CommentPartial", comments);
        }

        public ActionResult Details(int id)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.isUser = User.Identity.IsAuthenticated;
            var userId = User.Identity.GetUserId();
            var BookId = db.Books.Where(g => g.BookId == id);
            //var BookTitle = db.Books.Where(g => g.Title.Replace(" ", "-").ToLower() == title);
            var genres = db.Genres.ToList();
            var books = BookId.ToList();
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == id && c.ParentId == 0).OrderByDescending(d=>d.DateAdded).Take(20).ToList();
            ViewBag.countComments = db.Comments.Include(p => p.User).Where(c => c.BookId == id).Count();
            ViewBag.countReviews = db.Reviews.Include(p => p.User).Where(c => c.BookId == id).Count();

            var userReview = db.Reviews.Include(p => p.User).Where(c => c.BookId == id && c.UserId == userId).SingleOrDefault();

            var reviews = db.Reviews.Include(p => p.User).Include(p=>p.Rate).Where(c => c.BookId == id).OrderByDescending(d => d.DateAdded).Take(5).ToList();
            var newComment = new Comment();
            newComment.BookId = id;
            var newRate = new Rate();
            newRate.BookId = id;
            ViewBag.Title = BookId.Single().Title.ToString();

            double? rate = -1;
            
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
                Reviews = reviews,
                NewRate = newRate,
                userReview = userReview

                //SelectedBook = BookTitle

            };
            return View(vm);
        }
        
        public ActionResult ListGenres(string genrename, int? page)
        {
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var books = genre.Books.ToList();
            ViewBag.genrename = genrename;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.Title = genre.Name.ToString();
            //var books = db.Books.OrderByDescending(b => b.Title).ToList();

            return View(books.ToPagedList(pageNumber, pageSize));
        }
        [ChildActionOnly]
        public ActionResult GenresMenu(string genrename, int? page)
        {
            var genres = db.Genres.ToList();
            var author = db.Books.Select(p => p.Author).Distinct();
            var authors = db.Books.ToList();
            ViewBag.genrename = genrename;
            //int pageSize = 3;
            int pageNumber = (page ?? 1);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Wybierz sortowanie...", Value = "sort", Selected = true });
            items.Add(new SelectListItem { Text = "Tytuł - od A do Z", Value = "title_asc" });
            items.Add(new SelectListItem { Text = "Tytuł - od Z do A", Value = "title_desc" });
            items.Add(new SelectListItem { Text = "Ocena - od najniższej", Value = "ratings_asc" });
            items.Add(new SelectListItem { Text = "Ocena - od najwyższej", Value = "ratings_desc" });

            

            ViewBag.Sorting = items;
            //ViewBag.Sorting = new SelectList("ocena","ocena - malejaco");
            var VM = new HomeViewModel
            {
                //RatingsCheckBoxList = new List<CheckBoxItem>
                //{
                //    new CheckBoxItem {Value= true, Label="0-2,5"},
                //    new CheckBoxItem {Value= false, Label="2,6-4,5"},
                //    new CheckBoxItem {Value= false, Label="4,5-6,5"},
                //    new CheckBoxItem {Value= false, Label="6,6-8,5"},
                //    new CheckBoxItem {Value= false, Label="8,6-10"}
                //},
                Authors = authors,
                Author = author.ToList(),
                Genres = genres
            };

            return PartialView("_GenresMenu", VM);
        }
        
        public ActionResult FilterList(HomeViewModel model, string[] author1, int? page, string[] listAuthor, string genrename, string searchString, string currentFilter, double minRating, double maxRating, string sorting)
        {
            ViewBag.CurrentSort = sorting;
            var genres = db.Genres.ToList();
            //var author = db.Books.Select(p => p.Author).Distinct();
            //string[] temp = null;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //double minRating;
            //double maxRating;
            try
            {
                 minRating = Double.Parse(model.MinRating);
                 maxRating = Double.Parse(model.MaxRating);
            }
            catch
            {
                minRating = 0;
                maxRating = 10;
            }

            ViewBag.MinRating = minRating;
            ViewBag.MaxRating = maxRating;


            List<String> temp = new List<string>();
            //string[] authorSplit = author1.Split(',');
            string[] authorSplit = null;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
           
            if (author1 !=null)
            {
                if (author1.Count() > 1)
                {
                    authorSplit = author1;
                }
                else
                {
                    authorSplit = Regex.Split(author1[0], ",");
                
                }
            }
            else
            {
                authorSplit = db.Books.Where(p => p.Genre.Name == genrename).Select(p=>p.Author).Distinct().ToArray();
            }
           
            


            //int genre_start = UrlPath.LastIndexOf("/") + 1;
            //string genreName = UrlPath.Split('/').Last();
            //string genreName = UrlPath.Split('/').ElementAt(2);
            //string genreName = "Epos";
            ViewBag.listAuthor = author1;

            ViewBag.Title = genrename;

            //if (author1 != null)
            //{
            //    foreach (var item in author1)
            //    {
            //        temp.Add(item);
            //    }
            //    //ViewBag.Authors = author.Author1;
            //}

            IEnumerable<Book> selectedBook = null;

            var books = from b in db.Books
                        select b;
            //temp = "Adam Mickiewicz" + "," + "Homer";
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            if (!String.IsNullOrEmpty(searchString) && genre != null)
            {
               
                books = books.Where(a => a.Title.Contains(searchString) && 
                        a.Genre.Name.Contains(genrename) && 
                        authorSplit.Contains(a.Author) &&
                        a.AvgRating <= maxRating &&
                        a.AvgRating >= minRating
                        ).OrderByDescending(p=>p.Title);

                selectedBook = genre.Books.Where(a => authorSplit.Contains(a.Author) && a.Title.Contains(searchString) &&
                        getRating(a.BookId) <= maxRating &&
                        getRating(a.BookId) >= minRating).ToList();
                //selectedBook = genre.Books.Where(a => a.Title.Contains(searchString));


            }
            else if(genre != null)
            {

                books = books.Where(a=>a.Genre.Name.Contains(genrename) && 
                        authorSplit.Contains(a.Author) &&
                        a.AvgRating <= maxRating &&
                        a.AvgRating >= minRating
                       ).OrderByDescending(p => p.Title);
                selectedBook = genre.Books.Where(a => authorSplit.Contains(a.Author) &&
                        getRating(a.BookId) <= maxRating &&
                        getRating(a.BookId) >= minRating).ToList();
                //selectedBook = genre.Books.Where(a => a.Title.Contains(searchString));
               
            }
            switch (sorting)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    selectedBook = selectedBook.OrderByDescending(s => s.Author);
                    break;
                case "title_asc":
                    books = books.OrderBy(s => s.Title);
                    selectedBook = selectedBook.OrderBy(s => s.Author);
                    break;
                case "ratings_asc":
                    books = books.OrderBy(s => s.Ratings.Average(a => a.Value));
                    break;
                case "ratings_desc":
                    books = books.OrderByDescending(s => s.Ratings.Average(a => a.Value));
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }
            //var VM = new HomeViewModel
            //{

            //    //Author = author.ToList(),
            //    SelectedBook = selectedBook,
            //    Genres = genres
            //};

            //var data = db.Books.SqlQuery("select * from books where author in ("+temp+")").ToList();

            //string x = "";

            //if (temp != null)
            //{
            //    foreach (var df in temp)
            //    {
            //        if (String.IsNullOrEmpty(x))
            //        {
            //            x = "&autor="+df.Replace(" ", "%").ToLower();
            //        }
            //        else
            //        {
            //            x = x.Replace(" ", "%").ToLower() + "&autor=" + df.Replace(" ", "%").ToLower();
            //        }
            //    }
            //}
            ViewBag.Authors = author1;
            //Debug.WriteLine(x);
            ViewBag.selectedBooks = "selectedBook: "+temp+" "+ genrename+"data: ";
            ViewBag.FilterBook = selectedBook.ToPagedList(pageNumber, pageSize);
            return View("ListGenres", books.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index(string searchString, string genrename, int? page)
        {
            var books = from m in db.Books
                        select m;

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var genre = db.Genres.Include("Books").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            var bookVM = new HomeViewModel
            {
               
                LastBooks =  books.ToPagedList(pageNumber, pageSize)
            };
            ViewBag.genrename = genrename;
            return View(books);
        }

        //public ActionResult AddRate(int bookId)
        //{
        //    var newRate = new Rate();
        //    newRate.BookId = bookId;
        //    return View(newRate);
        //}
        [HttpPost]
        //public ActionResult AddRate(DetailsViewModels model, int bookId, string bookTitle)
        public ActionResult AddRate(DetailsViewModels model)
        {
            //model.NewRate.DateAdded = DateTime.Now;
            //model.NewRate.BookId = bookId;
            //model.NewRate.Value = 4;
            model.NewRate.UserId = User.Identity.GetUserId();
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            var bookTitle = db.Books.Where(p => p.BookId == model.NewRate.BookId).First().Title.Replace(" ", "-").ToLower().ToString();
            var book = db.Books.Where(p => p.BookId == model.NewRate.BookId).Single();
            
            //ViewBag.error = errors;
            if (ModelState.IsValid)
            {
                

                db.Ratings.Add(model.NewRate);
                db.SaveChanges();

                book.AvgRating = getRating(model.NewRate.BookId);
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            return RedirectToAction("Details", "Book", new { id = model.NewRate.BookId, _title = bookTitle });
        }


        public double getRating(int bookId)
        {
            double rating;
            var userId = User.Identity.GetUserId();
            try
            {
                //rate = db.Ratings.Where(r => r.BookId == id).Average(a => a.Value).ToString();
                rating = Math.Round(db.Ratings.Where(r => r.BookId == bookId).Average(a => a.Value), 2);
                

            }
            catch
            {
                rating = 0;
            }
            
            return rating;
        }
    }

    
}