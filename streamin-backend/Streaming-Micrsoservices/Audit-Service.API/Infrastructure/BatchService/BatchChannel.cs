using Audit_Service.API.Infrastructure.Persistence;
using System.Threading.Channels;

namespace Audit_Service.API.Infrastructure.BatchService
{
    public class BatchChannel
    {
        private readonly Channel<AuditEvent> _channel;
        public BatchChannel()
        {
            _channel = Channel.CreateBounded<AuditEvent>(new BoundedChannelOptions(20)
            {
                SingleReader= true,
                SingleWriter=false,
                FullMode=BoundedChannelFullMode.Wait
            });
        }
        public ChannelReader<AuditEvent> Reader => _channel.Reader;
        public async ValueTask<bool> TryWriteAsync(AuditEvent auditEvent, CancellationToken ct)
        {
            if (!await _channel.Writer.WaitToWriteAsync(ct))
            {
                return false;
            }

            return _channel.Writer.TryWrite(auditEvent);
        }

    }
}
