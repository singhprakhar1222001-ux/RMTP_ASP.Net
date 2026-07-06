using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Infrastructure.Messages.Topology
{
    public static class Topology
    {
        public const string ExchangeName = "user.exchange";
        public const string routingKey = "user.created";
        public const string QueueName = "workservice.user";
        public const string DLQ = "workservice.dlq";
    }
}
