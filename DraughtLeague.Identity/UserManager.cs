using System;
using System.Collections.Generic;
using System.Security;
using DraughtLeague.DAL.Extensions;
using DraughtLeague.DAL.Models;

namespace DraughtLeague.Identity
{
    public class UserManager : IDisposable
    {

        private static UserManager _userManager;
        private UserStore _userStore;

        private UserManager(string connectionString) {
            _userStore = UserStore.Create(connectionString);
        }

        public User User => _userStore.User;

        public UserManageResult CreateUser(string email, SecureString password) {
            User user = _userStore.LoadUserByEmail(email);
            if (user != null)
                return new UserManageResult {
                    Success = false,
                    Errors = new List<string> { "User already exists." }
                };

            bool passwordIsValid = validatePassword(password.Unsecure());
            if (!passwordIsValid)
                return new UserManageResult {
                    Success = false,
                    Errors = new List<string> { "Password does not meet minimum complexity requirements." }
                };

            byte[] salt = CryptoTools.CreateSalt();
            user = new User
            {
                Id = Guid.NewGuid(),
                Entropy = salt,
                EmailAddress = email.Trim(),
                PasswordHash = password.HashValue(salt)
            };
            _userStore.Add(user);
            _userStore.Update();

            return new UserManageResult
            {
                Success = true
            };

        }

        public UserManageResult SetPassword(string id, SecureString password)
        {
            bool passwordIsValid = validatePassword(password.Unsecure());
            if (!passwordIsValid)
                return new UserManageResult
                {
                    Success = false,
                    Errors = new List<string> { "Password does not meet minimum complexity requirements." }
                };

            byte[] salt = CryptoTools.CreateSalt();
            User user = _userStore.LoadUserById(id);
            user.Entropy = salt;
            user.PasswordHash = password.HashValue(salt);

            _userStore.Update();

            return new UserManageResult
            {
                Success = true
            };

        }


        public User FindById(string id)
        {
            return _userStore.LoadUserById(id);
        }

        public User FindByEmail(string email)
        {
            return _userStore.LoadUserByEmail(email);
        }

        public UserManageResult AddClaim(string type, string value) {
            User user = _userStore.User;
            if (user == null)
                return new UserManageResult {
                    Success = false,
                    Errors = new List<string> { "" }
                };

            user.Claims ??= new List<IdentityClaim>();

            user.Claims.Add(new IdentityClaim {
                Type = type,
                Value = value
            });

            _userStore.Update();

            return new UserManageResult {
                Success = true
            };
        }
        
        private bool validatePassword(string password)
        {

            if (password.Length >= 8)
                return true;
            return false;
        }

        public static UserManager Create(string connectionString)
        {

            if (_userManager != null)
                return _userManager;

            _userManager = new UserManager(connectionString);
            //{
            //    //UserLockoutEnabledByDefault = true,
            //    //DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5),
            //    //MaxFailedAccessAttemptsBeforeLockout = 5
            //};

            return _userManager;
        }



        //public class EmailService : IIdentityMessageService
        //{
        //    public Task SendAsync(IdentityMessage message)
        //    {
        //        // Plug in your email service here to send an email.
        //        return Task.FromResult(0);
        //    }
        //}

        //public class SmsService : IIdentityMessageService
        //{
        //    public Task SendAsync(IdentityMessage message)
        //    {
        //        // Plug in your SMS service here to send a text message.
        //        return Task.FromResult(0);
        //    }
        //}

        public void Dispose()
        {
            _userManager = null;
        }

    }
}
