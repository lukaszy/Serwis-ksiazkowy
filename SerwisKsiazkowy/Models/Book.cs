using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }  
        public int Pages { get; set; }
        public DateTime Published { get; set; }
        public int Rate { get; set; }
        public string CoverFileName { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}