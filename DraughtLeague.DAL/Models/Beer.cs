using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TapRoomId { get; set; }
        public int UntappdId { get; set; }
        public string BeerName { get; set; }
        public decimal? ABV { get; set; }
        public string BreweryName { get; set; }
        public string StyleFamily { get; set; }
        public string StyleName { get; set; }

        [ForeignKey("BarId")]
        public virtual TapRoom Bar { get; set; }

        public virtual ICollection<Tap> Taps { get; set; }

        [InverseProperty("Beer")]
        public virtual ICollection<CheckIn> CheckIns { get; set; }

    }
}
