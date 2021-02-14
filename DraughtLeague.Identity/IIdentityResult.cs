using System.Collections.Generic;

namespace DraughtLeague.Identity
{
    public interface IIdentityResult
    {
        bool Success { get; }
        IEnumerable<string> Errors { get; }
    }
}