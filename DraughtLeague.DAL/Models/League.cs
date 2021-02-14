using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DraughtLeague.DAL.Models
{
    public class League
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public byte? MaxRosterSize { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<LeagueMember> LeagueMembers { get; set; }

    }
}
