using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    [Table("Ratings")]
    public class Rate
    {
        [Key]
        public int RateId { get; set; }

        [Range(0, 10, ErrorMessage = "Wartość {0} musi być miedzy {1} a {2}.")]
        public int Value { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Review Review { get; set; }

       
    }
}