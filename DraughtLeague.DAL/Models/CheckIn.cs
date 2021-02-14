using System;
using System.ComponentModel.DataAnnotations;

namespace DraughtLeague.DAL.Models
{
    public class CheckIn
    {
        [Key]
        public int Id { get; set; }
        public int UntappdId { get; set; }
        public decimal Rating { get; set; }
        public DateTime Timestamp { get; set; }
        
        public virtual Beer Beer { get; set; }

    }
}
