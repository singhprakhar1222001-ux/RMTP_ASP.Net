using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Events
{
    public class ProjectUserCreatedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
