using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Persistance.Outbox;

namespace WorkService.Persistance
{
    public class ApplicationbDbContext(DbContextOptions<ApplicationbDbContext> options) : DbContext(options)
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationbDbContext).Assembly);
            modelBuilder.Entity<OutboxMessage>().HasKey(x => x.Id);
            //using reflections to find impplementations of IEntityTypeConfiguration, this is also the basis of mark interfaces
            
        }
        public DbSet<OutboxMessage> outbox;
    }
}
