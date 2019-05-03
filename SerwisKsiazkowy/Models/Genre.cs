using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Display(Name = "Gatunek")]
        public string Name { get; set; }

        //public virtual Book Book { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}