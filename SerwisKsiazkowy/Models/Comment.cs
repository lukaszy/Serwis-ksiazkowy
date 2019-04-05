﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Models
{
    
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }

        
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        

    }
}