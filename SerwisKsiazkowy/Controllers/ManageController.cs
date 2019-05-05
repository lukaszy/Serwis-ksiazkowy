﻿using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Add()
        {
            var addBook = new EditBookViewModel();
            var genres = db.Genres.ToArray();
            addBook.Genres = genres;

            return View(addBook);
        }
     
        [HttpPost]
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
    }
}