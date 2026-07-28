using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IIntegreationEvent
    {
        public Guid EventId {  get; }
        public DateTime OccuredOn { get; }

    }
}
