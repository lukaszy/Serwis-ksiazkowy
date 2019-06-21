using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class ReviewViewModel
    {     
        public Review Review { get; set; }
        [Display(Name = "Ocena")]
        public Rate Rate { get; set; }
        public bool isValueRate { get; set; }
        public int[] ValueRate = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}

//public IEnumerable<Review> ReviewsVM { get; set; }
//public IEnumerable<Rate> RatingsVM { get; set; }