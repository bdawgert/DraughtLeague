﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DraughtLeague.DAL.Models
{
    public class LeagueMember
    {
        [Key, Column(Order = 0)]
        public Guid TapRoomId { get; set; }
        [Key, Column(Order = 1)]
        public Guid LeagueId { get; set; }
        public string Role { get; set; }

        public virtual TapRoom TapRoom { get; set; }
        public virtual League League { get; set; }
    }
}
