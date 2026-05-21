using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.ProjectContext;

namespace WorkService.Infrastructure.Configuration
{
    internal class ProjectBaseConfiguration : IEntityTypeConfiguration<ProjectBase>
    {
        public void Configure(EntityTypeBuilder<ProjectBase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name).IsUnique();

        }
    }
}
