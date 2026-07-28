using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Persistance.Outbox;

namespace WorkService.Persistance
{
    public interface IMediatRProvider
    {
        
    }
    public class ApplicationbDbContext(DbContextOptions<ApplicationbDbContext> options,IMediator mediator) : DbContext(options),IMediatRProvider
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql();
        //}
        public readonly IMediator _mediator=mediator;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationbDbContext).Assembly);
            modelBuilder.Entity<OutboxMessage>().HasKey(x => x.Id);
            //using reflections to find impplementations of IEntityTypeConfiguration, this is also the basis of mark interfaces
            
        }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
