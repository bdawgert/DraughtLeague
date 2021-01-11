using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class Roster
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public int UntappdId { get; set; }
        public string BeerName { get; set; }
        public string BreweryName { get; set; }
        public string StyleFamily { get; set; }
        public string StyleName { get; set; }

        [ForeignKey("BarId")]
        public virtual Bar Bar { get; set; }

        public virtual ICollection<LineUp> LineUps { get; set; }

    }
}
