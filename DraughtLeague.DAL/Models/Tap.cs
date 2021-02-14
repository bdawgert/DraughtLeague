using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class Tap
    {
        [Key]
        public Guid Id { get; set; }
        public byte TapNumber { get; set; }
        public byte Round { get; set; }
        public Guid TapRoomId { get; set; }
        public Guid BeerId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int Points { get; set; }
        public bool IsFinalized { get; set; }
        public DateTimeOffset? LastUpdateDate { get; set; }
        public int LastUpdateId { get; set; }

        [ForeignKey("BeerId")]
        public virtual Beer Beer { get; set; }
        [ForeignKey("BarId")]
        public virtual TapRoom Bar { get; set; }

    }
}


