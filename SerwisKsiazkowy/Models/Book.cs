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
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Pole {0} musi zawierać co najmniej {1} znaków")]
        public string Title { get; set; }

        
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Pole {0} musi zawierać co najmniej {1} znaków")]
        public string Author { get; set; }

        [Display(Name = "Stron")]
        [Range(1, 10000, ErrorMessage = "Wartość {0} musi być z przedziału {1} a {2}.")]
        public int Pages { get; set; }

        [Display(Name = "Data wydania")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime YearPublished { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [Display(Name = "Wydawnictwo")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Pole {0} musi zawierać co najmniej {1} znaków")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [Display(Name = "Opis")]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "Pole {0} musi zawierać co najmniej {1} znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane")]
        [Display(Name = "Ocena")]
        public int Rate { get; set; }

        
        [Display(Name = "Okładka")]
        public string CoverFileName { get; set; }

        public double AvgRating { get; set; }

        public virtual Genre Genre { get; set; }
        

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Ratings { get; set; }
    }
}