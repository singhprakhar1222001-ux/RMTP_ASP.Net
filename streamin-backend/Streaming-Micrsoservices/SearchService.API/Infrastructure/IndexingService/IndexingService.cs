
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SearchService.API.IndexClass;
using SearchService.API.Infrastructure.Buffer;

namespace SearchService.API.Infrastructure.IndexingService
{
    public class IndexingService:BackgroundService
    {
        private readonly MessageBuffer _buffer;
        private readonly ElasticClient _client;
        public IndexingService(MessageBuffer buffer,ElasticClient client)
        {
            _buffer = buffer;
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            List<WorkIndexBody> workIndexBodies = new();
            await foreach (var doc in await _buffer.ReadAllIndexes(stoppingToken))
            {
                workIndexBodies.Add(doc);
                if (workIndexBodies.Count > 0)
                {
                    await FlushCall(workIndexBodies,stoppingToken);
                    workIndexBodies.Clear();
                }
            }
        }

        private async Task FlushCall(List<WorkIndexBody> docs,CancellationToken ct)
        {
            var operations = new List<IBulkOperation>();
            foreach(var doc in docs)
            {
                var operation = new BulkIndexOperation<WorkIndexBody>(doc)
                {
                    VersionType = VersionType.External,
                    Version=doc.Version,
                    Id=doc.Id
                };
                operations.Add(operation);
                
            }
            var bulkRequest = new BulkRequest("work-index")
            {
                Operations = operations
            };
            await _client.BulkUpload(bulkRequest, ct);
        }
    }
}
