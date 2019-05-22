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
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();

            //else
            //{
            //    ModelState.AddModelError("ErrorMessage", "Błąd");
            //}

            //return RedirectToAction("Details", "Book",new { id = bookId, _title = bookTitle });
            return PartialView("_Comment",comments);
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
        //public ActionResult Delete(int id, int bookId, string bookTitle)
        public ActionResult Delete(int id, int bookId)
        {
            Comment deleteComment = db.Comments.Find(id);
            db.Comments.Remove(deleteComment);

            db.SaveChanges();
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();

            // return RedirectToAction("Details", "Book", new { id = bookId, _title = bookTitle });
            return PartialView("_Comment", comments);
            //return Json(new { success = true });
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
                        
                        //username = x.Username

                    }).FirstOrDefault();
        }
        public JsonResult testAdd()
        {
            var dane = db.Comments.Where(x => x.ParentId != 0).Select(p=>new
            {
                CommentId=p.CommentId,
               
                Content = p.Content
            }).ToList();

            List<Comment> ObjEmp = new List<Comment>()
            {
                //Adding records to list
                new Comment {CommentId=1,Content="Vithal Wadje" },
                new Comment {CommentId=1,Content="Vithal Wadje" }
            };
            return Json(dane, JsonRequestBehavior.AllowGet);
            //return Json("hello", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reply(int id)
        {
            var dane = db.Comments.Include(p=>p.User).Where(x => x.ParentId == id).Select(p => new
            {
                CommentId = p.CommentId,

                Content = p.Content,
                DateAdded = p.DateAdded,
                UserId = p.User.UserName
            }).ToList();

            
            return Json(dane, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addNewComment(Comment comment)
        {
            //try
            //{
                var _comment = new Comment()
                {
                    ParentId = comment.ParentId,
                    Content = comment.Content,
                    UserId = User.Identity.GetUserId(),
                    DateAdded = DateTime.Now,
                    BookId = comment.BookId
                };
            try
            {
            db.Comments.Add(_comment);
                db.SaveChanges();
            }


            catch
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            //var model = AddComment(comment);
            //var model = db.Comments.Where(x => x.CommentId == _comment.CommentId)
            //    .Select(x => new Comment
            //    {
            //        CommentId = x.CommentId,
            //        Content = x.Content,
            //        ParentId = x.ParentId,
            //        DateAdded = x.DateAdded,
            //        BookId = x.BookId

            //        //username = x.Username

            //    }).FirstOrDefault();
            //var model = db.Comments.Where(x => x.CommentId == _comment.CommentId).ToList();

            return Json(true, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception)
            //{
            //    //Handle Error here..
            //}

            //return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult testowy()
        {
            var dane = db.Comments.Include(p => p.User).ToList();


            return PartialView("_Reply",dane);
        }

        public JsonResult DajOdpowiedzi(int id)
        {
            var dane = db.Comments.Where(x => x.CommentId == id).Select(x=> new
            {
                Content = x.Content
            }).ToList();
            return Json(dane,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildren(int id)
        {
            //double? rate = -1;
            
            var userId = User.Identity.GetUserId();

            var children = db.Comments.Where(r => r.ParentId == id).Count();
            var childrens = db.Comments.Where(r => r.ParentId == id).ToList();


            ViewBag.ValueChildren = children;
            return PartialView(childrens);
        }

        public ActionResult GetComments(int bookId)
        {
            //double? rate = -1;
            var comments = db.Comments.Include(p => p.User).Where(c => c.BookId == bookId && c.ParentId == 0).OrderByDescending(d => d.DateAdded).Take(10).ToList();

            return PartialView("_Comment",comments);
        }




    }
}