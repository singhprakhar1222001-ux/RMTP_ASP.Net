using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Messages.Topology
{
    public static class Topology
    {
        public const string ExchangeName = "user.created";
        public const string routingKey = "user.created";
    }
}
