using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.WorkContext.WorkDomainEvents
{
    public class AddCommentDomainEvent(
        string comment,
        Guid CommenterId,
        Guid WorkId,
        Guid ProjectId,
        string Name
        ):DomainEvents
    {
        public Guid CommenterId { get; init; } = CommenterId;
        public string Comment { get; init; } = comment;
        public Guid WorkId { get; init; } = WorkId;
        public Guid ProjectId { get; init; } = ProjectId;
        public string Name { get; init; } = Name;
    }
}
