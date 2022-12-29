using ARMApocalypseSASAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ARMApocalypseSASAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Survivor> Survivors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<SurvivorInfectionReport> SurvivorInfectionReports { get; set; }


    }
}
