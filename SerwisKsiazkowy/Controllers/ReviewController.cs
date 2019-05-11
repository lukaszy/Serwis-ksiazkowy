using Microsoft.AspNet.Identity;
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
    public class ReviewController : Controller
    {
        BookContext db = new BookContext();
        // GET: Review
        public ActionResult Index(int bookId, string bookTitle)
        {
            ViewBag.bookId = bookId;
            ViewBag.bookTitle = bookTitle.Replace(" ", "-").ToLower().ToString();
            

            return View();
        }

        [HttpPost]
        public ActionResult Add(Review model, int bookId, string bookTitle)
        {
            
            model.DateAdded = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Reviews.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }

        [HttpPost]
        public ActionResult Delete(int id, int bookId, string bookTitle)
        {
            Review deleteReview = db.Reviews.Find(id);
            db.Reviews.Remove(deleteReview);

            db.SaveChanges();

            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }
    }
}