namespace Library.Controllers
{
    using Library.Domain;
    using Library.Models;
    using Library.Service.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using Microsoft.Owin.Security.Cookies;
    using System.Web.Mvc;

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public ActionResult Register()
        {
            return View(new LogInViewModel());
        }
            
        [HttpPost]
        public ActionResult Register(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!this._accountService.IsReaderExist(logInViewModel.Email))
            {
                this._accountService.Create(new Reader { Email = logInViewModel.Email });
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Role, "reader"),
                        new Claim(ClaimTypes.Email, logInViewModel.Email)
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(logInViewModel.ReturnUrl));
            }
            else
            {
                ModelState.AddModelError("", "This email already in use.");
                return View(logInViewModel);
            }
        }

        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Reader user = this._accountService.GetReaderByEmail(logInViewModel.Email);
            if (user != null)
            {
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Email, logInViewModel.Email)
                }, "ApplicationCookie");


                if (user.Email.Equals("admin@admin.com"))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "reader")); 
                }

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(logInViewModel.ReturnUrl));
            }
            else
            {
                ModelState.AddModelError("", "Invalid email");
                return View(logInViewModel);
            }
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Book");
            }

            return returnUrl;
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
      
        public ActionResult UnregisteredPage()
        {
            return View();
        }
	}
}