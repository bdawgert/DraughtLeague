using System.Configuration;
using DraughtLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DraughtLeague.DAL
{
    public class LeagueDbContext : DbContext
    {
        public LeagueDbContext(string connectionString) : base(GetOptions(connectionString)) { }

        public LeagueDbContext(DbContextOptions options) : base(options) { }

        private static DbContextOptions GetOptions(string connectionString) {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<IdentityClaim> IdentityClaims { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<LeagueMember> LeagueMembers { get; set; }
        public virtual DbSet<SeasonMember> SeasonMembers { get; set; }
        public virtual DbSet<TapRoom> TapRooms { get; set; }
        public virtual DbSet<Beer> Beers { get; set; }
        public virtual DbSet<Tap> Taps { get; set; }

        public virtual DbSet<CheckIn> CheckIns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeagueMember>()
                .HasKey(c => new { c.TapRoomId, c.LeagueId });
            modelBuilder.Entity<SeasonMember>()
                .HasKey(c => new { c.TapRoomId, c.SeasonId });

            modelBuilder.Entity<CheckIn>()
                .HasOne(x => x.Beer)
                .WithMany(x => x.CheckIns)
                .HasForeignKey(x => x.UntappdId)
                .HasPrincipalKey(x => x.UntappdId);

        }
    }
}
