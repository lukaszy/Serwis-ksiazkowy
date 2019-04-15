using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Book> LastBooks { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Book> SelectedBook { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

    }
}