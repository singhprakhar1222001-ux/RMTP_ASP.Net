using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Domain.WorkContext
{
    public class Workitem
    {
        private Workitem() { }//for efcore

        private Workitem(string Name, string description, string comment,Guid ProjectId, Guid assignedId, Guid managerId, DateOnly deadline)
        {
            Id=Guid.NewGuid();
            this.Name=Name; 
            this.description=description;
            this.comment=comment;
            this.assignedId=assignedId;
            this.managerId=managerId;
            this.Deadline=deadline;
            this.ProjectId=ProjectId;
            DateOnly AssignedDate = new DateOnly(year: DateTime.Now.Year, month: DateTime.Now.Month, day: DateTime.Now.Day);
        }
         
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Guid ProjectId { get; private set; }
        public string description { get; private set; }

        public string comment { get; private set; }

        public Guid assignedId { get; private set; }

        public Guid managerId { get; private set; }

        public DateOnly AssignmentDate { get; private set; }

        public DateOnly Deadline {get; private set; }

        public static Workitem CreateWorkItem(string Name, string description, string comment,Guid ProjectId, Guid assignedId, Guid managerId,DateOnly Deadline)
        {
            //some validation
            return new Workitem(Name, description, comment, ProjectId,assignedId,managerId,Deadline);



            //some event
        }


        //start with simple assignment events
        

        
    }
}
