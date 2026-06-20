using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.Abstraction;
using Contracts.WorkService.Consts;

namespace Work_Service.Domain.WorkContext
{
    public class Workitem:Entity
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
            this.WorkStatus = WorkStatus.InProgress;
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
        public bool IsOverDue { get; private set; }
        public WorkStatus WorkStatus { get; private set; }

        public static Workitem CreateWorkItem(string Name, string description, string comment,Guid ProjectId, Guid assignedId, Guid managerId,DateOnly Deadline)
        {
            var CurrentDateTime = DateTime.UtcNow.AddDays(3);
            DateOnly checkDate = DateOnly.FromDateTime(CurrentDateTime);
            if (Deadline < checkDate)
            {
                throw new DomainException("Deadline has to be more than 3 days from now");
            }
            return new Workitem(Name, description, comment, ProjectId,assignedId,managerId,Deadline);
        }

        public void SubmitForApproval()
        {
            if (this.WorkStatus != WorkStatus.InProgress)
            {
                throw new DomainException("Approval cannot be requested for incorrect state");
            }
            this.WorkStatus = WorkStatus.PendingApproval;
            //some domain event we can raise
        }
        public void Approve(string reason)
        {
            if(this.WorkStatus != WorkStatus.PendingApproval)
            {
                throw new DomainException("Approval cannot be provided for something that hasnt asked");
            }
            this.WorkStatus = WorkStatus.Done;
            
        }
        public void Denied(string reason)
        {
            if (this.WorkStatus != WorkStatus.PendingApproval)
            {
                throw new DomainException("Approval cannot be provided for something that hasnt asked");
            }
            this.WorkStatus = WorkStatus.Denied;
        }
        public void Reopen(DateOnly deadline)
        {
            if (this.WorkStatus != WorkStatus.Denied)
            {
                throw new DomainException("ReOpen Has to be given to work that is denied");
            }
            this.WorkStatus = WorkStatus.InProgress;
            this.Deadline = deadline;
        }
        public void Abandon()
        {
            this.WorkStatus = WorkStatus.Abandoned;
            //indicate a terminal finish
        }
        
    }
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }  
    }
   
}
