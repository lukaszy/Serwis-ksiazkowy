﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "Wprowadź e-mail.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło.")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

    }
}