namespace Audit_Service.API.Infrastructure.Messaging.Topology
{
    public static class Topology
    {
        public const string EventQueue = "audit.queue";
        public const string routingKey = "workservice.audit.#";
        public const string retryQueue30s = "audit.retry-30s";
        public const string retryQueue60s = "audit.retry-60s";
        public const string retryQueue90s = "audit.retry-90s";
        public const string retryExchange30s = "retry-exchange-30s";
        public const string retryExchange60s = "retry-exchange-60s";
        public const string retryExchange90s = "retry-exchange-90s";
        public const string DLQqueue = "audit.dlq";


    }
}
