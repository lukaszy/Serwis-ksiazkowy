using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace SerwisKsiazkowy.Controllers
{
    public class ManageController : Controller
    {
        BookContext db = new BookContext();
        // GET: Manage
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author" ? "author_desc" : "author";
            ViewBag.GenreSortParm = sortOrder == "genre_name" ? "genre_name_desc" : "genre_name";
            ViewBag.RateSortParm = sortOrder == "rate" ? "rate_desc" : "rate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


             var books = from b in db.Books
                    select b;
            

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            { 
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                case "author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "genre_name":
                    books = books.OrderBy(s => s.Genre.Name);
                    break;
                case "genre_name_desc":
                    books = books.OrderByDescending(s => s.Genre.Name);
                    break;
                case "rate":
                    books = books.OrderBy(s => s.Ratings.Average(a => a.Value));
                    break;
                case "rate_desc":
                    books = books.OrderByDescending(s => s.Ratings.Average(a => a.Value));
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
            //return View(books.ToList());
            //var books = db.Books.ToList();
            //return View(books);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? bookId, bool? confirmSuccess)
        {
            //Book books = db.Books.Find(id);
            //var VM = new HomeViewModel
            //{
            //    SelectedBook = books
            //};
            var editBook = new EditBookViewModel();
            var genres = db.Genres.ToArray();
            editBook.Genres = genres;
            editBook.ConfirmSuccess = confirmSuccess;

            Book b;
            if (!bookId.HasValue)
            {
                b = new Book();
            }
            else
            {
                b = db.Books.Find(bookId);
            }

            editBook.Book = b;

            return View(editBook);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(HttpPostedFileBase file, EditBookViewModel model)
        {

            if (file != null && file.ContentLength > 0)
            {
                var fileExt = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid() + fileExt;

                var path = Path.Combine(Server.MapPath("~/Content/Covers/"), filename);
                file.SaveAs(path);
 
                model.Book.CoverFileName = filename;

            }

            db.Entry(model.Book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { confirmSuccess = true });

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            var addBook = new EditBookViewModel();
            var genres = db.Genres.ToArray();
            addBook.Genres = genres;

            return View(addBook);
        }
     
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(HttpPostedFileBase file, EditBookViewModel model)
        {
            
            var f = Request.Form;
            if (file != null && file.ContentLength > 0)
            {
               
                var fileExt = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid() + fileExt;

                var path = Path.Combine(Server.MapPath("~/Content/Covers/"), filename);
                file.SaveAs(path);
              
                model.Book.CoverFileName = filename;

                db.Entry(model.Book).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Index", new { confirmSuccess = true });
            }
            else
            {
                ModelState.AddModelError("", "Nie wskazano pliku.");
                var genres = db.Genres.ToArray();
                model.Genres = genres;
                return View(model);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, string title)
        {
            Book deleteBook = new Book();
            deleteBook.BookId = id;
            deleteBook.Title = title;
            return View(deleteBook);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Book deleteBook = db.Books.Find(id);
            db.Books.Remove(deleteBook);

            var deleteComments = db.Comments.Where(c => c.BookId == id);
            foreach(var el in deleteComments)
            {
                db.Comments.Remove(el);
            }

            //db.Entry(model.Book).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { confirmSuccess = true });

        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddGenre()
        {
            

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddGenre(Genre genreModel)
        {
            //Genre addGenre = db.Genres.Find();

            var genre = from m in db.Genres
                        select m;
            

            if (!String.IsNullOrEmpty(genreModel.Name))
            {
                genre = genre.Where(s => s.Name.Contains(genreModel.Name));
                if(genre.Count()>0)
                {
                    return RedirectToAction("Index", new { confirmSuccess = false });
                }else
                {
                    if (ModelState.IsValid)
                    {
                        db.Genres.Add(genreModel);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", new { confirmSuccess = true });
                }
                
            }
            else
            {
                return RedirectToAction("Index", new { confirmSuccess = false });
            }
            
            
            //db.Entry(model.Book).State = EntityState.Deleted;
           // db.SaveChanges();
            

        }
    }
}