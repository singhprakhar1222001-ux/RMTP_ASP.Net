using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.ProjectContext
{
    public class ProjectBase:Entity
    {
        public ProjectBase() { }
        private ProjectBase(Guid guid, string Name, string Description, Guid ProjectHead)
        {
            Id = guid;
            this.Name = Name;
            this.Description = Description;
            this.ProjectHead = ProjectHead;
        }
        public Guid Id { get;}
        public string Name { get; private set; }

        public string Description { get; private set; }

        public Guid ProjectHead { get;private set; }

        

        public static ProjectBase? CreateProject(string Name, string Description, Guid Projecthead)
        {
            if(Name.Length<7 || Name[0] >90 || Name[0]<65)
            {
                throw new UserCreationException("User validation failed");
            }
            Guid guid = Guid.NewGuid();
            return new ProjectBase(guid, Name, Description, Projecthead);
            
        }

        
    }


    public enum Role
    {
        Manager, 
        SubManager,
        Employee,
    }
    public record UserDetails(string Name, Role Role);

    public class UserCreationException : Exception
    {
        public UserCreationException(string message) : base(message)
        {

        }
    }
}
