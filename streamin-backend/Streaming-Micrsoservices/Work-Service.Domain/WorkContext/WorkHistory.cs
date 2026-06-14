using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.WorkService.Primitives;

namespace Work_Service.Domain.WorkContext
{
    public class WorkHistory
    {
        private WorkHistory() { }
        public Guid id {  get;}
        public DateTime LogTime {  get; private set; }

        public Guid WorkItemID { get; private set; }
        public Guid AssignedId { get; private set; }

        public string Assignecomment { get; private set; }

        public string ManagerComment { get; private set; }

        public ProgressStates PreviousState { get; private set; }

        public ProgressStates CurrentState { get; private set; }

        internal static WorkHistory? CreateWorkHistory()
        {
            return new WorkHistory();
        }


    }
}
