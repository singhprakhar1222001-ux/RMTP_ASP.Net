using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Consts
{
    public enum WorkStatus
    {
        InProgress = 1,
        PendingApproval = 2,
        Denied = 3,
        Done = 4,
        Abandoned = 5
    }
}
