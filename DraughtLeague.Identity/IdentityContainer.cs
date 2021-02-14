
using DraughtLeague.DAL.Models;

namespace DraughtLeague.Identity
{
    public class IdentityContainer
    {

        public IdentityContainer(string connectionString) {
            UserManager = UserManager.Create(connectionString);
            SignInManager = SignInManager.Create(UserManager);
        }
        
        public UserManager UserManager { get; }
        public SignInManager SignInManager { get; }

    }
}
