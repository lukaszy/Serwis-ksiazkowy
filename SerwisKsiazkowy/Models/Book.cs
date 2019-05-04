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
        [Display(Name = "Gatunek")]
        public int GenreId { get; set; }
        //[Required(AllowEmptyStrings = true)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Stron")]
        public int Pages { get; set; }

        [Display(Name = "Data wydania")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime YearPublished { get; set; }

        [Display(Name = "Wydawnictwo")]
        public string Publisher { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Ocena")]
        public int Rate { get; set; }

        [Display(Name = "Okładka")]
        public string CoverFileName { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Ratings { get; set; }
    }
}