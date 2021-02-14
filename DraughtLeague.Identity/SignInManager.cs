using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DraughtLeague.DAL.Extensions;
using DraughtLeague.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace DraughtLeague.Identity
{
    public class SignInManager : IDisposable
    {

        private static SignInManager _signInManager;
        private UserManager _userManager;

        private SignInManager(UserManager userManager)
        {
            _userManager = userManager;
        }

        public static SignInManager Create(UserManager userManager)
        {
            if (_signInManager == null)
                _signInManager = new SignInManager(userManager);
            return _signInManager;
        }

        public SignInResult PasswordSignIn(string email, SecureString password, bool shouldLockout)
        {
            User user = _userManager.FindByEmail(email);
            if (user == null || user.UserStatus == User.IdentityUserStatus.Deleted) {
                return SignInResult.NotAllowed;
            }

            bool authenticated = user.PasswordHash == password.HashValue(user.Entropy);
            if (authenticated)
                return SignInResult.Success;
         
            return SignInResult.Failed;
        }


        public SignInResult AutoSignIn(string email) {
            User user = _userManager.FindByEmail(email);
            
            return SignInResult.Success;

        }

        public ClaimsPrincipal CreateUserPrincipal(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, ".default"),
            };

            if (user.Claims != null)
                claims.AddRange(user.Claims.Select(x => new Claim(x.Type, x.Value)));

            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(userIdentity);
        }

        public void Dispose()
        {
            _signInManager = null;
        }
    }
}