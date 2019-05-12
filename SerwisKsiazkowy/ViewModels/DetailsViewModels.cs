using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisKsiazkowy.ViewModels
{
    public class DetailsViewModels 
    {
        
        public IEnumerable<Book> SelectedBook { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        public double? Ratings { get; set; }

        public IEnumerable<Rate> UserRate { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public Comment NewComment { get; set; }
        public Rate NewRate { get; set; }
        public int[] ValueRate = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //public int Rate { get; set; }
       
    }
    public class New_Comment
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class NewRate
    {
        public int Value { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}