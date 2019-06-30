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
using Newtonsoft.Json;

namespace SerwisKsiazkowy.Controllers
{
    public class CommentController : Controller
    {
        BookContext db = new BookContext();
       
        public ActionResult ListComments(int id,  int? page)
        {
            ViewBag.countComments = db.Comments.Include(p => p.User).Where(c => c.BookId == id).Count();
            var bookTitle = db.Comments.Include(p => p.Book).Where(p => p.BookId == id).Select(p => p.Book.Title);
           
            var comments = db.Comments.Include(p => p.User).Include(p => p.Book).Where(c => c.BookId == id).OrderByDescending(d => d.DateAdded).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
            //return View(comments);
        }
        public ActionResult Add(int bookId)
        {
            var newComment = new Comment();
            ViewBag.isUser = User.Identity.IsAuthenticated;
            newComment.BookId = bookId;
            ViewBag.CommentCount = db.Comments.Where(c => c.BookId == bookId).Count();
            return View(newComment);
        }

        [HttpPost]
        public ActionResult Add(HomeViewModel model, int bookId, string bookTitle)
        {
            model.NewComment.DateAdded = DateTime.Now;
            model.NewComment.UserId = User.Identity.GetUserId();
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            
            if (ModelState.IsValid)
            {
                db.Comments.Add(model.NewComment);
                db.SaveChanges();
            }
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();

          
            ViewBag.CommentCount = db.Comments.Where(c => c.BookId == bookId).Count();
            return PartialView("_Comment",comments);
        }




        public ActionResult AddInAll(int bookId, string bookTitle)
        {
            ViewBag.isUser = User.Identity.IsAuthenticated;
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
         

            return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int bookId)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            Comment deleteComment = db.Comments.Find(id);
            db.Comments.Remove(deleteComment);

            db.SaveChanges();
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();

            ViewBag.CommentCount = db.Comments.Where(c => c.BookId == bookId).Count();
            return PartialView("_Comment", comments);
        }

        public Comment AddComment(Comment comment)
        {
            var _comment = new Comment()
            {
                ParentId = comment.ParentId,
                Content = comment.Content,
                UserId = User.Identity.GetUserId(),
                DateAdded = DateTime.Now,
                BookId = 1
            };

            db.Comments.Add(_comment);
            db.SaveChanges();
            return db.Comments.Where(x => x.CommentId == _comment.CommentId)
                    .Select(x => new Comment
                    {
                        CommentId = x.CommentId,
                        Content = x.Content,
                        ParentId = x.ParentId,
                        DateAdded = x.DateAdded,
                        BookId = x.BookId
                        

                    }).FirstOrDefault();
        }
       

        public ActionResult GetComments(int bookId)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();
            ViewBag.CommentCount = db.Comments.Where(c => c.BookId == bookId).Count();
            return PartialView("_Comment",comments);
        }

        public ActionResult GetAllComments(int bookId, int? page)
        {
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.BookId = 1;
            TempData["TestVal"] = 1;
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;
            return View("_AllComments",comments.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComment(int id, int bookId, int? page)
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.IsAdmin = isAdmin;

            Comment deleteComment = db.Comments.Find(id);
            db.Comments.Remove(deleteComment);

            db.SaveChanges();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).ToList();
            return View("_AllComments", comments.ToPagedList(pageNumber, pageSize));
        }



    }
}