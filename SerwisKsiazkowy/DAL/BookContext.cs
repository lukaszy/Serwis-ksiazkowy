﻿using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace SerwisKsiazkowy.DAL
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookContext")
        {

        }

        static BookContext()
        {
            Database.SetInitializer<BookContext>(new BookInitializer());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}