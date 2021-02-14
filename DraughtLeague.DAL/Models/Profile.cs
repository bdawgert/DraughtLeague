using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; }

    }
}
