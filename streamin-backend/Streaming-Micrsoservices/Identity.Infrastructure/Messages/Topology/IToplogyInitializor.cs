using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Messages.Topology
{
    public interface IToplogyInitializor
    {
        public Task Initialize();
        public IChannel GetChannel();
    }
}
