using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class TapRoom
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
        public virtual ICollection<Tap> Taps { get; set; }

        public virtual LeagueMember LeagueMember { get; set; }
        public virtual SeasonMember SeasonMember { get; set; }

        [NotMapped]
        public virtual League League => LeagueMember?.League;
        [NotMapped]
        public virtual Season Season => SeasonMember?.Season;

    }
}
