using System.Configuration;
using DraughtLeague.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DraughtLeague.DAL
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() { }

        public EFDbContext(string connectionString) : base(GetOptions(connectionString)) {
            
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        public virtual DbSet<Roster> Roster { get; set; }


    }
}
