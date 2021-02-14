using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public byte[] Entropy { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public byte AccessFailedCount { get; set; }
        public bool Approved { get; set; }
        public IdentityUserStatus UserStatus { get; set; }
        public DateTimeOffset? LastLogin { get; set; }

        public virtual ICollection<IdentityClaim> Claims { get; set; }
        [ForeignKey("Id")]
        public virtual Profile Profile { get; set; }

        //[ForeignKey("SenderId")]
        //public virtual ICollection<Match> SendingMatches { get; set; }
        //[ForeignKey("RecipientId")]
        //public virtual ICollection<Match> ReceivingMatches { get; set; }

        //public virtual ICollection<SignUp> SignUps { get; set; }
        //public virtual ICollection<MatchPreference> MatchPreferences { get; set; }

        public enum IdentityUserStatus
        {
            NotApproved = -1,
            None = 0,
            Approved = 1,
            Deleted = -99

        }
    }
}
