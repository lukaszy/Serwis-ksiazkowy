﻿using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SerwisKsiazkowy.Controllers
{
    public class HomeController : Controller
    {
        private BookContext db = new BookContext();

        

        //public ActionResult Index()
        //{
        //    //Genre newGenre = new Genre { Name = "Biografie" };
        //    //db.Genres.Add(newGenre);
        //    //db.SaveChanges();

        //    // var booksList = db.Books.ToList();

        //    var genres = db.Genres.ToList();

        //    var lastBooks = db.Books.OrderByDescending(a => a.Title).ToList();

        //    var vm = new HomeViewModel()
        //    {
        //        Genres = genres,
        //        LastBooks = lastBooks

        //    };
                
        //    return View(vm);
        //}

        public async Task<ActionResult> Index(string searchString)
        {
            var books = from m in db.Books
                        select m;
            var genres = db.Genres.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            var bookVM = new HomeViewModel
            {
                Genres = genres,
                LastBooks = await books.ToListAsync()
            };

            return View(bookVM);
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}