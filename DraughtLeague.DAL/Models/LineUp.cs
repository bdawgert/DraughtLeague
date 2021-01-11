using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class LineUp
    {
        [Key]
        public Guid Id { get; set; }
        public byte Round { get; set; }
        public byte Tap { get; set; }
        public Guid RosterId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int Points { get; set; }
        public bool IsFinalized { get; set; }
        public DateTimeOffset? LastUpdateDate { get; set; }
        public int LastUpdateId { get; set; }

        [ForeignKey("RosterId")]
        public virtual Roster Roster { get; set; }

    }
}


