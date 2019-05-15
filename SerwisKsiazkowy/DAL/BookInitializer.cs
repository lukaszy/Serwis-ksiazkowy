using SerwisKsiazkowy.Models;
using SerwisKsiazkowy.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using SerwisKsiazkowy.Migrations;
using System.Text;
using System.Threading.Tasks;

namespace SerwisKsiazkowy.DAL
{
    public class BookInitializer : MigrateDatabaseToLatestVersion<BookContext, Configuration>
    {
        //protected override void Seed(BookContext context)
        //{
        //    SeedBookData(context);

        //    base.Seed(context);
        //}

        public static void SeedBookData(BookContext context)
        {

            var genres = new List<Genre>
            {
                new Genre() {GenreId = 1, Name = "Epos"},
                new Genre() {GenreId = 2, Name = "Dramat"},
                new Genre() {GenreId = 3, Name = "Powiesc"}
            };
            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book() {BookId = 1, GenreId = 1, Title = "Pan Tadeusz", Author = "Adam Mickiewicz", Pages = 1000, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "1.png", Description = "Pan Tadeusz, czyli Ostatni zajazd na Litwie to epopeja narodowa z elementami gawędy szlacheckiej powstała w latach 1832–1834 w Paryżu. Składa się z dwunastu ksiąg pisanych wierszem, trzynastozgłoskowym aleksandrynem polskim. Czas akcji: pięć dni z roku 1811 i dwa dni z roku 1812."},
                new Book() {BookId = 2, GenreId = 2, Title = "Dziady", Author = "Adam Mickiewicz", Pages = 1000, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "2.png", Description = "Trwa proces studentów – filomatów. Wileńskie więzienie jest pełne młodych ludzi, którzy otwarcie przyznają się do tego, że są Polakami. Jeden z nich, Konrad, staje się wcieleniem niepodległości własnej ojczyzny. Nadchodzą niespodziewane wydarzenia, w których ważną rolę odegrają siły pozaziemskie, a na ziemi po raz kolejny zetrą się szatan i Bóg."},
                new Book() {BookId = 3, GenreId = 3, Title = "Krzyżacy", Author = "Henryk Sienkiewicz", Pages = 1000, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "no_image.png", Description = "Trwa proces studentów – filomatów. Wileńskie więzienie jest pełne młodych ludzi, którzy otwarcie przyznają się do tego, że są Polakami. Jeden z nich, Konrad, staje się wcieleniem niepodległości własnej ojczyzny. Nadchodzą niespodziewane wydarzenia, w których ważną rolę odegrają siły pozaziemskie, a na ziemi po raz kolejny zetrą się szatan i Bóg."},
                new Book() {BookId = 4, GenreId = 1, Title = "Odyseja", Author = "Homer", Pages = 800, YearPublished = new DateTime(2005, 10, 4), Publisher = "Wydawnictwo Rea", Rate = 5, CoverFileName = "no_image.png", Description = "Trwa proces studentów – filomatów. Wileńskie więzienie jest pełne młodych ludzi, którzy otwarcie przyznają się do tego, że są Polakami. Jeden z nich, Konrad, staje się wcieleniem niepodległości własnej ojczyzny. Nadchodzą niespodziewane wydarzenia, w których ważną rolę odegrają siły pozaziemskie, a na ziemi po raz kolejny zetrą się szatan i Bóg."}

            };
            books.ForEach(b => context.Books.AddOrUpdate(b));
            context.SaveChanges();

           

           

            var comments = new List<Comment>
            {
                new Comment() {CommentId = 1, Content = "fajna", BookId = 1, DateAdded = new DateTime(2019,04,15), UserId = "811fb950-b699-444c-a9b3-81254a8da378"}, 
                new Comment() {CommentId = 2, Content = "nudna", BookId = 1, DateAdded = new DateTime(2019,04,14), UserId = "811fb950-b699-444c-a9b3-81254a8da378"}
            };
            comments.ForEach(c => context.Comments.AddOrUpdate(c));
            context.SaveChanges();

            var rate = new List<Rate>
            {
                new Rate() {RateId = 1, Value = 3, BookId = 1, UserId = "811fb950-b699-444c-a9b3-81254a8da378"}
                //new Rate() {RateId = 2, Value = 5, BookId = 2, UserId = "811fb950-b699-444c-a9b3-81254a8da378"}
            };
            rate.ForEach(c => context.Ratings.AddOrUpdate(c));
            context.SaveChanges();

            //var reviews = new List<Review>
            //{
            //    new Review() {ReviewId = 1, Content = " 1 recenzja", BookId = 1, DateAdded = new DateTime(2019,04,14), UserId = "811fb950-b699-444c-a9b3-81254a8da378",RateValue = 3 }
            //};
            //reviews.ForEach(r => context.Reviews.AddOrUpdate(r));
            //context.SaveChanges();

            
        }
    }
}