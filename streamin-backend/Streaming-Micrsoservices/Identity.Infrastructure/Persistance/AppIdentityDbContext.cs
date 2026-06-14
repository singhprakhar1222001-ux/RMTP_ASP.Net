using Identity.Infrastructure.Outbox;
using Identity.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistance
{
    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshTokenStore>(
                b => b.HasKey(b => b.Id));

            builder.Entity<OutboxMessage>(
                b => {
                    b.HasKey(b => b.Id);
                    });

            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.EnableNotifications).HasDefaultValue(true);
            });
            builder.HasDefaultSchema("userschema");

            
            
            
        }
        public DbSet<RefreshTokenStore> RefreshTokens { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }

    
}
