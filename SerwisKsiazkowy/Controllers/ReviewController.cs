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
        public ActionResult Index(int bookId)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            var bookTitle = db.Books.Where(p => p.BookId == bookId).First().Title.Replace(" ", "-").ToLower().ToString();
            var userId = User.Identity.GetUserId();
            ViewBag.bookId = bookId;
            ViewBag.bookTitle = bookTitle.Replace(" ", "-").ToLower().ToString();
            Rate userRate = new Rate();
            Review review = new Review();
            review.BookId = bookId;
            bool isValue;
            
            try
            {
                userRate = db.Ratings.Where(r => r.BookId == bookId && r.UserId == userId).Single();
                isValue = true;
            }
            catch
            {
                userRate.BookId = bookId;
                isValue = false;
            }
            
            ReviewViewModel VM = new ReviewViewModel()
            {
                isValueRate = isValue,
                Rate = userRate,
                Review = review
            };
            

            return View(VM);
        }

        public ActionResult ListReviews(int id, string _title, int? page)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.countReviews = db.Reviews.Include(p => p.User).Where(c => c.BookId == id).Count();
            ViewBag.BookTitle = _title.ToUpper();
            var reviews = db.Reviews.Include(p => p.User).Include(p=>p.Books).Where(c => c.BookId == id).OrderByDescending(d => d.DateAdded).ToList();
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(reviews.ToPagedList(pageNumber, pageSize));
            //return View(comments);
        }

        [HttpPost]
        public ActionResult Add(ReviewViewModel model, bool isValue)
        {

            var bookTitle = db.Books.Where(p => p.BookId == model.Review.BookId).First().Title.Replace(" ", "-").ToLower().ToString();
            var book = db.Books.Where(p => p.BookId == model.Review.BookId).Single();
            model.Review.DateAdded = DateTime.Now;
            model.Review.UserId = User.Identity.GetUserId();
            bool isRate = false;
            model.Rate.UserId = User.Identity.GetUserId();
            if (isValue == false)
            {
                model.Rate.BookId = model.Review.BookId;
                model.Review.Rate = model.Rate;
                isRate = false;
            }
            else
            {
                //model.Review.Rate = model.Rate;
                isRate = true;
            }

          
            if (ModelState.IsValid)
            {
                
                if (isRate == false)
                {
                    db.Reviews.Add(model.Review);
                    //db.Ratings.Add(model.Rate);
                    
                } else
                {
                    
                    //db.SaveChanges();
                    db.Entry(model.Rate).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Reviews.Add(model.Review);
                    model.Review.Rate = model.Rate;
                    //db.Entry(model.Review).State = EntityState.Modified;

                   
                    //db.Reviews.Attach(model.Review);
                    // model.Review.Rate = model.Rate;
                    //db.Entry(model.Review).State = EntityState.Modified;
                }
                
                db.SaveChanges();

                book.AvgRating = getRating(model.Review.BookId);
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return RedirectToAction("Details", "Book", new { id = model.Review.BookId, _title = bookTitle });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int bookId, string bookTitle)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            Review deleteReview = db.Reviews.Find(id);
            db.Reviews.Remove(deleteReview);

            db.SaveChanges();

            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }

        
        public ActionResult Edit(int reviewId, string bookTitle)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.bookId = db.Reviews.Where(p =>p.ReviewId == reviewId).Select(p=>p.BookId).Single();
            ViewBag.bookTitle = bookTitle.Replace(" ", "-").ToLower().ToString();
            var editReview = new ReviewViewModel();
            var ratings = db.Ratings.ToArray();
            editReview.RatingsVM = ratings;
            //editBook.ConfirmSuccess = confirmSuccess;

            Review b = db.Reviews.Find(reviewId);
            var userRate = db.Ratings.Where(p => p.RateId == reviewId).Count();
            if(b.Rate == null && userRate>0)
            {
                editReview.isValueRate = false;
            }
        

            editReview.Review = b;

            return View(editReview);
            
        }

        [HttpPost]
        public ActionResult Edit(ReviewViewModel model)
        {
            var bookTitle = db.Books.Where(p => p.BookId == model.Review.BookId).First().Title.Replace(" ", "-").ToLower().ToString();
            model.Review.DateAdded = DateTime.Now;
            db.Entry(model.Review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Book", new { id = model.Review.BookId, _title = bookTitle });

        }

        [ChildActionOnly]
        public ActionResult GetReviewRate(int bookId, string userId)
        {
            //double? rate = -1;
            string rate = "";
            
            try
            {
                rate = db.Ratings.Where(r => r.BookId == bookId && r.UserId == userId).Select(p=>p.Value).First().ToString();

            }
            catch
            {
                rate = "Brak";
            }
            ViewBag.Value = rate;
            return PartialView("GetRate");
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