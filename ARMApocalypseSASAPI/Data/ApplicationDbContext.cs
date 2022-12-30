using ARMApocalypseSASAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ARMApocalypseSASAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       // protected override void OnConfiguring
       //(DbContextOptionsBuilder optionsBuilder)
       // {
       //     optionsBuilder.UseInMemoryDatabase(databaseName: "arm8622apocalypse");
       // }

        public DbSet<Survivor> Survivors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<SurvivorInfectionReport> SurvivorInfectionReports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Item>()
                .HasData(
                new Item { Id = Guid.NewGuid().ToString(), Name = "Water", IsActive = true, IsDeleted = false, Price = 4 },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Food", IsActive = true, IsDeleted = false, Price = 3 },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Medication", IsActive = true, IsDeleted = false, Price = 2 },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Ammunition", IsActive = true, IsDeleted = false, Price = 1 });
        }


    }
}
