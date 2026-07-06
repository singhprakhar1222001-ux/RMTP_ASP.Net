
using RabbitMQ.Client;
using SearchService.API.IndexClass;
using System.Threading.Channels;

namespace SearchService.API.Infrastructure.Buffer

{
    public class MessageBuffer
    {
        private readonly Channel<WorkIndexBody> workChannel = Channel.CreateBounded<WorkIndexBody>(new BoundedChannelOptions(500));
        
        public async Task AddMessage(WorkIndexBody message) {
            var writer = workChannel.Writer;
            await writer.WriteAsync(message);
        }

        public async Task<IAsyncEnumerable<WorkIndexBody>> ReadAllIndexes(CancellationToken ct)
        {
            var reader=workChannel.Reader;  
            var res=reader.ReadAllAsync();
            return res;
        }
    }
}
