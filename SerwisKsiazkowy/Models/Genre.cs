using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}