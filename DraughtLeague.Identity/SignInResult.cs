using System;
using System.Collections.Generic;

namespace DraughtLeague.Identity
{
    public class SignInResult : IIdentityResult
    {
        public bool Success { get; set; }
        public SignInStatus Status { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Flags]
        public enum SignInStatus
        {
            HasAccount = 1,
            Authenticated = 2,
            LockedOut = 8

        }

    }
}