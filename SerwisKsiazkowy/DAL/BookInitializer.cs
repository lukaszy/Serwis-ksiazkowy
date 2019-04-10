using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace SerwisKsiazkowy.DAL
{
    public class BookInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            SeedBookData(context);

            base.Seed(context);
        }

        private void SeedBookData(BookContext context)
        {
            var genres = new List<Genre>
            {
                new Genre() {GenreId = 1, Name = "Powiesc" },
                new Genre() {GenreId = 1, Name = "Podróże" }
            };
            genres.ForEach(g => context.Genres.Add(g));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book() {BookId = 1, GenreId = 1, Title = "Pan Tadeusz", Author = "Adam Mickiewicz", Pages = 1000, Published = new DateTime(2005, 10, 4), Rate = 5, CoverFileName = "1.png"},
                new Book() {BookId = 2, GenreId = 1, Title = "Dziady", Author = "Adam Mickiewicz", Pages = 1000, Published = new DateTime(2005, 10, 4), Rate = 5, CoverFileName = "2.png"}
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment() {CommentId = 1, Content = "fajna", BookId = 1},
                new Comment() {CommentId = 2, Content = "nudna", BookId = 1}
            };
            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();

            var reviews = new List<Review>
            {
                new Review() {ReviewId = 1, Content = "jakas 1 recenzja", BookId = 1},
                new Review() {ReviewId = 2, Content = "jakas 2 recenzja", BookId = 1 }
            };
            reviews.ForEach(r => context.Reviews.Add(r));
            context.SaveChanges();
        }
    }
}