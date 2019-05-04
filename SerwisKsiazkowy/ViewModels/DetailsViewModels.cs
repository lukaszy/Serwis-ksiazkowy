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
       
    }
    public class New_Comment
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }
}