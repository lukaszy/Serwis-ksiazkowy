using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Treść musi zawierać co najmniej {1} znaków")]
        [Display(Name = "Treść")]
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }


        public int BookId { get; set; }
        public virtual Book Books { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Rate Rate { get; set; }
       
        [Display(Name = "Ocena")]
        [Range(0, 10, ErrorMessage = "Wartość {0} musi być między {1} a {2}.")]
        public int RateValue { get; set; }
        
        

    }
}