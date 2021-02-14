using System.Linq;
using DraughtLeague.DAL;
using DraughtLeague.DAL.Models;

namespace DraughtLeague.Identity
{
    public class UserStore
    {
        private static UserStore _userStore;

        private LeagueDbContext _dal;

        private UserStore(string connectionString) {

            _dal = new LeagueDbContext(connectionString);
        }

        public User User { get; set; }

        public User LoadUserByEmail(string email)
        {
            return User = _dal.Users.FirstOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());
        }

        public User LoadUserById(string id)
        {
            return User = _dal.Users.Find(id);
        }

        public User Add(User user)
        {
            return User = _dal.Users.Add(user).Entity;
        }

        public static UserStore Create(string connectionString)
        {
            if (_userStore == null)
                _userStore = new UserStore(connectionString);
            return _userStore;
        }

        public static UserStore Recreate(string connectionString)
        {
            _userStore = new UserStore(connectionString);
            return _userStore;
        }

        public void Update()
        {
            _dal.SaveChanges();
        }


    }
}