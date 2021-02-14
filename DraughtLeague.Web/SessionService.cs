using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DraughtLeague.DAL;
using DraughtLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DraughtLeague.Web
{
    public class SessionService {
        private LeagueDbContext _dal;
        private Guid _userId;

        public SessionService(IHttpContextAccessor httpContextAccessor, LeagueDbContext dbContext) {
            _dal = dbContext;

            string userId = httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (userId != null)
                initialize(userId);
                
        }
        
        public LeagueDbContext DAL => _dal;

        public bool IsValid { get; set; }
        public Guid UserId => _userId;
        public string Username { get; set; }
        public string EmailAddress { get; set; }


        private void initialize(string userId) {
            if (Guid.TryParse(userId, out _userId)) {
                User user = DAL.Users.Where(x => x.Id == _userId).Include(x => x.Claims).Include(x => x.Profile).FirstOrDefault();
                if (user == null)
                    return;

                IsValid = true;
                Username = user.Username;
                EmailAddress = user.EmailAddress;
            }

        }

    }
}

