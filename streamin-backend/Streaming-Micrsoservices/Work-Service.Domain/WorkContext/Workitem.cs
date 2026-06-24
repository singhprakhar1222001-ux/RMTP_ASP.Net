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
        private HashSet<Comments> Comments;

        private Workitem(string Name, string description, string comment,Guid ProjectId, Guid assignedId, Guid managerId, DateOnly deadline)
        {
            Id=Guid.NewGuid();
            this.Name=Name; 
            this.description=description;
            this.assignedId=assignedId;
            this.managerId=managerId;
            this.Deadline=deadline;
            this.ProjectId=ProjectId;
            DateOnly AssignedDate = new DateOnly(year: DateTime.Now.Year, month: DateTime.Now.Month, day: DateTime.Now.Day);
            this.WorkStatus = WorkStatus.InProgress;
            this.Version = 1;
            Comments commentobj = new Comments(comment:comment, userId:managerId, workId:Id);
            this.Comments = new () {commentobj};
            
        }
         
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Guid ProjectId { get; private set; }
        public string description { get; private set; }

        public IReadOnlyList<Comments> _Comment => Comments.ToList();//has to be updated as comment may be a cascading effect

        public Guid assignedId { get; private set; }

        public Guid managerId { get; private set; }

        public DateOnly AssignmentDate { get; private set; }

        public DateOnly Deadline {get; private set; }
        public bool IsOverDue { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
        public int Version { get; private set; }

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

        public void SubmitForApproval(string comment)
        {
            if (this.WorkStatus != WorkStatus.InProgress)
            {
                throw new DomainException("Approval cannot be requested for incorrect state");
            }
            this.WorkStatus = WorkStatus.PendingApproval;
            this.Comments.Add(
                new Comments(comment:comment, userId:this.assignedId,workId:this.Id)
                );
            UpdateVersion();
            //some domain event we can raise
        }
        public void Approve(string reason)
        {
            if(this.WorkStatus != WorkStatus.PendingApproval)
            {
                throw new DomainException("Approval cannot be provided for something that hasnt asked");
            }
            this.WorkStatus = WorkStatus.Done;
            this.Comments.Add(
                new Comments(reason, userId: this.managerId, workId: this.Id));
            UpdateVersion();
            
        }
        public void Denied(string reason)
        {
            if (this.WorkStatus != WorkStatus.PendingApproval)
            {
                throw new DomainException("Approval cannot be provided for something that hasnt asked");
            }
            this.WorkStatus = WorkStatus.Denied;
            this.Comments.Add(
                new Comments(reason,userId: this.managerId, workId: this.Id)
                );

            UpdateVersion();
        }
        public void Reopen(DateOnly deadline)
        {
            if (this.WorkStatus != WorkStatus.Denied)
            {
                throw new DomainException("ReOpen Has to be given to work that is denied");
            }
            this.WorkStatus = WorkStatus.InProgress;
            this.Deadline = deadline;
            UpdateVersion();
        }
        public void Abandon()
        {
            this.WorkStatus = WorkStatus.Abandoned;
            UpdateVersion();
            //indicate a terminal finish
        }
        private void UpdateVersion()
        {
            this.Version++;
            return;
        }
        
    }
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }  
    }
   
}
