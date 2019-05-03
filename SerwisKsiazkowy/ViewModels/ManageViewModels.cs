using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class EditBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public bool? ConfirmSuccess { get; set; }
    }
}