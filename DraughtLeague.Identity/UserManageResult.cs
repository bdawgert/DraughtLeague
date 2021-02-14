using System.Collections.Generic;

namespace DraughtLeague.Identity
{
    public class UserManageResult : IIdentityResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}