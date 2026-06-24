using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.WorkContext;

namespace WorkService.Persistance.Configuration
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<Workitem>
    {
        public void Configure(EntityTypeBuilder<Workitem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.WorkStatus)
                .HasConversion<string>();

            builder.HasMany<Comments>()
                .WithOne()
                .HasForeignKey(c => c.WorkId);
            
        }
    }
}
