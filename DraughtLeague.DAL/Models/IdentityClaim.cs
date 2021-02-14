using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class IdentityClaim
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
