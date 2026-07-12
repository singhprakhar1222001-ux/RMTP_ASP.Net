namespace SearchService.API.Infrastructure.Messaging.Topology
{
    public static class Topology
    {
        public const string EventQueue = "search.queue";
        public const string RetryQueue = "search.retry";
        public const string routingKey = "workservice.#";
        public const string retryQueue = "search.retry";
        public const string retryExchange = "retry-exchange";

    }
}
