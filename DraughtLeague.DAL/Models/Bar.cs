using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DraughtLeague.DAL.Models
{
    public class Bar
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Roster> Rosters { get; set; }

    }
}
