using Microsoft.EntityFrameworkCore;

namespace asyncawait.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Resource> Resources { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Area1"},
                new Area { Id = 2, Name = "Area2"},
                new Area { Id = 3, Name = "Area3"},
                new Area { Id = 4, Name = "Area4"},
                new Area { Id = 5, Name = "Area5"});

            modelBuilder.Entity<Company>().HasData(
                new Area { Id = 1, Name = "Company1"},
                new Area { Id = 2, Name = "Company2"},
                new Area { Id = 3, Name = "Company3"},
                new Area { Id = 4, Name = "Company4"},
                new Area { Id = 5, Name = "Company5"});

            modelBuilder.Entity<Resource>().HasData(
                new Area { Id = 1, Name = "Resource1"},
                new Area { Id = 2, Name = "Resource2"},
                new Area { Id = 3, Name = "Resource3"},
                new Area { Id = 4, Name = "Resource4"},
                new Area { Id = 5, Name = "Resource5"});
        }
    }
}
