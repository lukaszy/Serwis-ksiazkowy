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
        public IEnumerable<Book> Authors { get; set; }

        public List<String> Author { get; set; }
        public string[] Author1 { get; set; }
    }
}