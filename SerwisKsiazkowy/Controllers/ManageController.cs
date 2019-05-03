using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisKsiazkowy.Controllers
{
    public class ManageController : Controller
    {
        BookContext db = new BookContext();
        // GET: Manage
        public ActionResult Index()
        {
            var books = db.Books.ToList();
            return View(books);
        }

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
        public ActionResult Edit(HttpPostedFileBase file, EditBookViewModel model)
        {
            if (model.Book.BookId > 0)
            {
                // Saving existing entry

                db.Entry(model.Book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { confirmSuccess = true });
            }
            else
            {
                var f = Request.Form;

                

                db.Entry(model.Book).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index", new { confirmSuccess = true });
            }
        }
        public ActionResult Add()
        {
            //IEnumerable<SelectListItem> items = db.Genres.Select(c => new SelectListItem
            //{
            //    Value = c.GenreId.ToString(),
            //    Text = c.Name

            //});
            var addBook = new EditBookViewModel();
            var genres = db.Genres.ToArray();
            addBook.Genres = genres;

            return View(addBook);
        }

        //public ActionResult Add()
        //{
        //    IEnumerable<SelectListItem> items = db.Genres.Select(c => new SelectListItem
        //    {
        //        Value = c.Name,
        //        Text = c.Name

        //    });
        //    ViewBag.GenreName = items;
        //    return View();
        //}
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file, EditBookViewModel model)
        {
            
            var f = Request.Form;

            db.Entry(model.Book).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index", new { confirmSuccess = true });

        }
    }
}