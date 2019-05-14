using Microsoft.AspNet.Identity;
using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SerwisKsiazkowy.Controllers
{
    public class CommentController : Controller
    {
        BookContext db = new BookContext();
        // GET: Comments
        //public ActionResult Index([Bind(Prefix ="Id")]int BookId)
        //{
        //    var book = db.Books.Find(BookId);
        //    if( book !=null)
        //    {
        //        return View(book);
        //    }
        //    return HttpNotFound();
        //}
        public ActionResult ListComments(int id, string _title, int? page)
        {
            ViewBag.countComments = db.Comments.Include(p => p.User).Where(c => c.BookId == id).Count();
            ViewBag.BookTitle = _title.ToUpper();
            var comments = db.Comments.Include(p => p.User).Include(p => p.Book).Where(c => c.BookId == id).OrderByDescending(d => d.DateAdded).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
            //return View(comments);
        }
        public ActionResult Add(int bookId)
        {
            var newComment = new Comment();
            newComment.BookId = bookId;
            return View(newComment);
        }

        [HttpPost]
        public ActionResult Add(HomeViewModel model, int bookId, string bookTitle)
        {
            model.NewComment.DateAdded = DateTime.Now;
            model.NewComment.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Comments.Add(model.NewComment);
                db.SaveChanges();
            }
            //else
            //{
            //    ModelState.AddModelError("ErrorMessage", "Błąd");
            //}

            return RedirectToAction("Details", "Book",new { id = bookId, _title = bookTitle });
        }




        public ActionResult AddInAll(int bookId, string bookTitle)
        {
            ViewBag.bookId = bookId;
            ViewBag.bookTitle = bookTitle;
            
            return View();
        }
        [HttpPost]
        public ActionResult AddInAll(Comment model, int bookId, string bookTitle)
        {
            model.DateAdded = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Comments.Add(model);
                db.SaveChanges();
            }
            //else
            //{
            //    ModelState.AddModelError("ErrorMessage", "Błąd");
            //}

            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }


        [HttpPost]
        public ActionResult Delete(int id, int bookId, string bookTitle)
        {
            Comment deleteComment = db.Comments.Find(id);
            db.Comments.Remove(deleteComment);

            db.SaveChanges();
            
            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }



        
    }
}