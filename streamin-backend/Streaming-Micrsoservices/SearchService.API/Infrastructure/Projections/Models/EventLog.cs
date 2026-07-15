namespace SearchService.API.Infrastructure.Projections.Models
{
    public class EventLog
    {
        public Guid EventId { get; init; }
        public DateTime OccuredOn { get; init; }
    }
}
