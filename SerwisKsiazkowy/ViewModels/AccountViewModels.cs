using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wprowadź e-mail.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło.")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Hasło musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditProfileViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i potwierdzenia hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }
}