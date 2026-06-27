using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using SearchService.API.IndexClass;

namespace SearchService.API.Infrastructure.IndexingService
{
    public class ElasticClient
    {
        private readonly SemaphoreSlim sem_lock = new SemaphoreSlim(1, 1);
        ElasticsearchClient client = new ElasticsearchClient(new Uri("ElasticConnectionString"));

        public async Task GetIndex()
        {
            if (client.Indices.Exists("work-index") == null)
            {
                try
                {
                    await sem_lock.WaitAsync();
                    var res = await client.Indices.CreateAsync("work-index");
                }
                catch { }
                finally { sem_lock.Release(); }
            }
        }

        public async Task BulkUpload(BulkRequest bulkRequest,CancellationToken ct)
        {
            var response = await client.BulkAsync(bulkRequest);
        }
    }
}
