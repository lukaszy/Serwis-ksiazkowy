using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.DAL
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookContext")
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}