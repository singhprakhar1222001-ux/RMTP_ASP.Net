using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Infrastructure.Messages.Topology
{
    public interface ITopologyInitializer
    {
        public Task Initialize();
    }
}
