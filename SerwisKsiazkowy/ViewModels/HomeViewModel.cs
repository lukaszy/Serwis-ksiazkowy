using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerwisKsiazkowy.Infrastructure;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace SerwisKsiazkowy.ViewModels
{
    public class HomeViewModel
    {
        
        public IPagedList<Book> LastBooks { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IPagedList<Book> SelectedBook { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
        public IEnumerable<Book> Authors { get; set; }

        public List<String> Author { get; set; }
        public string autor { get; set; }
        
        public string[] Author1 { get; set; }
        //public IEnumerable<String> Author1 { get; set; }

        public double? Ratings { get; set; }

        public IEnumerable<Rate> UserRate { get; set; }


        //public List<CheckBoxItem> RatingsCheckBoxList { get; set; }
        
        public string MaxRating { get; set; }
        public string MinRating { get; set; }

    }
    public class NewComment
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }
    public class CheckBoxItem
    {
        public bool Value { get; set; }
        public string Label { get; set; }
    }
    public enum Values : byte
    {
        [Display(Name = "0-2,5")] Value1,
        [Display(Name = "2,6-4,5")] Value2,
        [Display(Name = "4,5-6,5")] Value3,
        [Display(Name = "6,6-8,5")] Value4,
        [Display(Name = "8,6-10")] Value5,
    }
    
}