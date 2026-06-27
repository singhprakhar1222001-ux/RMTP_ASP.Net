using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Events
{
    public class ProjectCreatedEvent
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
    }
}
