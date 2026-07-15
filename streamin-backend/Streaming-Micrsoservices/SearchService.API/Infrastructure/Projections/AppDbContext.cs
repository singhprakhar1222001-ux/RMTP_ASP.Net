using Microsoft.EntityFrameworkCore;
using SearchService.API.Infrastructure.Projections.Models;

namespace SearchService.API.Infrastructure.Projections
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUserProjection>().HasKey(x => x.UserId);
            modelBuilder.Entity<ProjectProjection>().HasKey(x => x.ProjectId);
            modelBuilder.Entity<EventLog>().HasIndex(x => x.EventId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProjectProjection> ProjectProjections { get; set; }
        public DbSet<ProjectUserProjection> ProjectUserProjections { get; set; } 
        public DbSet<EventLog> EventLogs { get; set; }

    }
}
