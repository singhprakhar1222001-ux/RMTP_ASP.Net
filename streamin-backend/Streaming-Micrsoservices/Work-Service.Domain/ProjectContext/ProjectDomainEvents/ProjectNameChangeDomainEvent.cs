using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.ProjectContext.ProjectDomainEvents
{
    public class ProjectNameChangeDomainEvent:DomainEvents
    {
        string oldName { get;set;}
        string newName { get;set;}
        Guid ChangePerson {  get;set;}
    }
}
