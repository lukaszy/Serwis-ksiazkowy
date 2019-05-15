using Microsoft.AspNet.Identity;
using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

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

        public ActionResult ListReviews(int id, string _title, int? page)
        {
            ViewBag.countReviews = db.Reviews.Include(p => p.User).Where(c => c.BookId == id).Count();
            ViewBag.BookTitle = _title.ToUpper();
            var reviews = db.Reviews.Include(p => p.User).Include(p=>p.Books).Where(c => c.BookId == id).OrderByDescending(d => d.DateAdded).ToList();
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(reviews.ToPagedList(pageNumber, pageSize));
            //return View(comments);
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

        
        public ActionResult Edit(int reviewId, string bookTitle)
        {
            var editReview = new ReviewViewModel();
            var ratings = db.Ratings.ToArray();
            editReview.RatingsVM = ratings;
            //editBook.ConfirmSuccess = confirmSuccess;

            Review b = db.Reviews.Find(reviewId);
           
            
        

            editReview.Review = b;

            return View(editReview);
            
        }

        [HttpPost]
        public ActionResult Edit(ReviewViewModel model)
        {

            model.Review.DateAdded = DateTime.Now;
            db.Entry(model.Review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Home", new { confirmSuccess = true });

        }
    }
}