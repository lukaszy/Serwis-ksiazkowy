﻿using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerwisKsiazkowy.Infrastructure;


namespace SerwisKsiazkowy.ViewModels
{
    public class HomeViewModel
    {
        
        public IEnumerable<Book> LastBooks { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Book> SelectedBook { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
        public IEnumerable<Book> Authors { get; set; }

        public List<String> Author { get; set; }
        public string autor { get; set; }
        //public string[] Author1 { get; set; }
        public IEnumerable<String> Author1 { get; set; }


    }
    public class NewComment
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }
    
}