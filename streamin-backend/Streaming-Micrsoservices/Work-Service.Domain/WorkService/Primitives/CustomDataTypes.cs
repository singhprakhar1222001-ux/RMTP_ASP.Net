using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Domain.WorkService.Primitives
{
    public enum ProgressStates
    {
        Start,
        Queued,
        Open,
        InReview,
        Overdue,
        Completed,
    }
}
