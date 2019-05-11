using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class ReviewViewModel
    {
        public IEnumerable<Review> ReviewsVM { get; set; }
        public IEnumerable<Rate> RatingsVM { get; set; }
    }
}