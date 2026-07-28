using Contracts.WorkService.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.WorkContext;
using Work_Service.Domain.WorkContext.WorkDomainEvents;

namespace Work_Service.Application.WorkContext
{
    internal class CommentDomainEventHandler:INotificationHandler<AddCommentDomainEvent>
    {
        private readonly IUnitofWork<Workitem> _unitOfWork;
        public CommentDomainEventHandler(IUnitofWork<Workitem> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddCommentDomainEvent domainEvent,CancellationToken ct)
        {
            ChangeEvent changeEvent = new ChangeEvent(
               EventId: Guid.NewGuid(),
               occuredon: DateTime.UtcNow,
               workId: domainEvent.WorkId,
               WorkName: domainEvent.Name,
               ProjectId: domainEvent.ProjectId,
               ActorId: domainEvent.CommenterId,
               AuditPayload: new AuditPayload(
                   comment: domainEvent.Comment,
                   change:null
                   )
               );
            _unitOfWork.AddOutbox(changeEvent);
        }

    }
}
