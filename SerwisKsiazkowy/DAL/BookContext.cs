using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SerwisKsiazkowy.DAL
{
    public class BookContext : IdentityDbContext<ApplicationUser>
    {
        public BookContext() : base("BookContext")
        {

        }

        static BookContext()
        {
            Database.SetInitializer<BookContext>(new BookInitializer());
        }

        public static BookContext Create()
        {
            return new BookContext();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rate> Ratings { get; set; }

    }
}