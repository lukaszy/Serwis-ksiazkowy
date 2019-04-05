using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string GenreId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }  
        public int Pages { get; set; }
        public DateTime Published { get; set; }
        public int Rate { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

    }
}