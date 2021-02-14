using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DraughtLeague.DAL.Extensions;
using DraughtLeague.Identity;
using DraughtLeague.Web.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DraughtLeague.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(SessionService sessionService) : base(sessionService) { }

        public IActionResult Index() {
            return View();
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        [Route("/Login")]
        [Route("/Account/Login")]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginVM());
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl) {
            if (!ModelState.IsValid)
                return View(model);

            UserManager userManager = UserManager.Create(_dal.Database.GetConnectionString());
            SignInManager signInManager = SignInManager.Create(userManager);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            SignInResult signInResult = signInManager.PasswordSignIn(model.EmailAddress, model.Password.Secure(), false);

            if (signInResult.Succeeded) {
                ClaimsPrincipal userPrincipal = signInManager.CreateUserPrincipal(userManager.User);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                return redirectToLocal(returnUrl);
            }

            if (signInResult.IsLockedOut)
                return View("Lockout");

            ModelState.AddModelError("", "Invalid Username or Password");
            return View(model);

        }

        //
        // GET: /Account/Logout
        [AllowAnonymous]
        [Route("/Logout")]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("", "");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid) {
                UserManager userManager = UserManager.Create(_dal.Database.GetConnectionString());
                UserManageResult userManagerResult = userManager.CreateUser(model.EmailAddress, model.Password.Secure());

                if (userManagerResult.Success) {
                    userManager.AddClaim("http://21brews.com/identity/claims/account-state", "registered");

                    ClaimsPrincipal userPrincipal = SignInManager.Create(userManager).CreateUserPrincipal(userManager.User);

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(userPrincipal));

                    //HttpContext.User = new ClaimsPrincipal(userPrincipal);
                    //FormsAuthentication.SetAuthCookie(userPrincipal.Identity.Name, false);
                    //HttpContext.User = userPrincipal;

                    //if (!_dal.Profiles.Any(x => x.Id == userPrincipal.Identity.Name))
                    //    return RedirectToAction("", "Profile");

                    return RedirectToAction("", "");
                }
                //addErrors(userManagerResult);
            }

            return View(model);
        }

        private ActionResult redirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

    }

}
