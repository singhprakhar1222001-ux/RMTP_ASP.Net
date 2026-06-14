using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Identity
{
    public interface IIntegreationEvent
    {
        Guid EventId {  get; }
        DateTime OccuredOn { get; }

    }
}
