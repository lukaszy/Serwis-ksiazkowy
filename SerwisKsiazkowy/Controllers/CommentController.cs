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
            return RedirectToAction("Details", "Book",new { id = bookId, _title = bookTitle });
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