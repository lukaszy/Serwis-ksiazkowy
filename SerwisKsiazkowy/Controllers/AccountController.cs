using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using SerwisKsiazkowy.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SerwisKsiazkowy.ViewModels;
using SerwisKsiazkowy.Models;
using System.Threading.Tasks;

namespace SerwisKsiazkowy.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("loginerror", "Nieprawidłowa próba logowania.");
                    return View(model);
            }

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserData = new UserData() };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Aby uzyskać więcej informacji o sposobie włączania potwierdzania konta i resetowaniu hasła, odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=320771
                    // Wyślij wiadomość e-mail z tym łączem
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Potwierdź konto", "Potwierdź konto, klikając <a href=\"" + callbackUrl + "\">tutaj</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
           
            return View(model);
            

        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserProfile()
        {
            ApplicationUser editUser = new ApplicationUser();
            
            editUser = UserManager.FindByName(User.Identity.Name);
            EditProfileViewModel user = new EditProfileViewModel();
            
            
            user.Email = editUser.Email;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityResult result = null;
            
            string id = User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            try
            {
               result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            }
            catch
            {
                result = null;
            }

            var check = await UserManager.CheckPasswordAsync(user, model.OldPassword);
            var setEmail = await UserManager.SetEmailAsync(User.Identity.GetUserId(), model.Email);
            if (setEmail.Succeeded && check)
            {
                 user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index","Home");
            }
            AddErrors(result);
            return View(model);
        }
        //[HttpPost]
        //public async Task<ActionResult> UserProfile(UserData model)
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var book = new <ApplicationUser>(new ApplicationDbContext());
        //    var manager = new UserManager<ApplicationUser>(book);
        //    var currentUser = UserManager.FindByName(User.Identity.Name);
        //    currentUser.UserData.FirstName = model.FirstName;
        //    currentUser.UserData.LastName = model.LastName;

        //    //await manager.UpdateAsync(currentUser);
        //    //var ctx = store.Context;
        //    //ctx.SaveChanges();

        //    return RedirectToAction("ListUser");
        //}
    }
}