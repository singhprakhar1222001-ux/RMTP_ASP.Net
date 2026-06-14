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
    internal class WorkHistoryConfiguration:IEntityTypeConfiguration<WorkHistory>
    {
        public void Configure(EntityTypeBuilder<WorkHistory> builder)
        {
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.Assignecomment);
        }
    }
}
