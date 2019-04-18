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
                new Genre() {GenreId = 1, Name = "Epos"},
                new Genre() {GenreId = 2, Name = "Dramat"}
            };
            genres.ForEach(g => context.Genres.Add(g));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book() {BookId = 1, GenreId = 1, Title = "Pan Tadeusz", Author = "Adam Mickiewicz", Pages = 1000, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "1.png", Description = "Pan Tadeusz, czyli Ostatni zajazd na Litwie to epopeja narodowa z elementami gawędy szlacheckiej powstała w latach 1832–1834 w Paryżu. Składa się z dwunastu ksiąg pisanych wierszem, trzynastozgłoskowym aleksandrynem polskim. Czas akcji: pięć dni z roku 1811 i dwa dni z roku 1812."},
                new Book() {BookId = 2, GenreId = 2, Title = "Dziady", Author = "Adam Mickiewicz", Pages = 1000, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "2.png", Description = "Trwa proces studentów – filomatów. Wileńskie więzienie jest pełne młodych ludzi, którzy otwarcie przyznają się do tego, że są Polakami. Jeden z nich, Konrad, staje się wcieleniem niepodległości własnej ojczyzny. Nadchodzą niespodziewane wydarzenia, w których ważną rolę odegrają siły pozaziemskie, a na ziemi po raz kolejny zetrą się szatan i Bóg."}
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

           

           

            var comments = new List<Comment>
            {
                new Comment() {CommentId = 1, Content = "fajna", BookId = 1, DateAdded = new DateTime(2019,04,15)}, 
                new Comment() {CommentId = 2, Content = "nudna", BookId = 1, DateAdded = new DateTime(2019,04,14)}
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