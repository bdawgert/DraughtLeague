using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class Season
    {
        [Key]
        public Guid Id { get; set; }
        public Guid LeagueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public DateTimeOffset DraftDate { get; set; }
        public int DraftRounds { get; set; }
        
        public DateTimeOffset WaiverStartDate { get; set; }
        public DateTimeOffset WaiverEndDate { get; set; }
        public int? MaxWaivers { get; set; }

        public virtual ICollection<SeasonMember> SeasonMembers { get; set; }
        [ForeignKey("LeagueId")]
        public virtual League League { get; set; }

    }
}
