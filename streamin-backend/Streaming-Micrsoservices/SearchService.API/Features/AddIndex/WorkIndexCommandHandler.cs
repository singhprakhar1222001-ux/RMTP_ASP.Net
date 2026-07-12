using Contracts.WorkService.Consts;
using Contracts.WorkService.Events;
using Contracts.WorkService.RoutingEventDirectory;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SearchService.API.IndexClass;
using SearchService.API.Infrastructure.Buffer;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.Messaging.Connection;
using SearchService.API.Infrastructure.Messaging.Topology;
using SearchService.API.Infrastructure.Projections.Models;
using System.Text;
using System.Text.Unicode;

namespace SearchService.API.Features.AddIndex
{

    public class WorkIndexCommandHandler : INotificationHandler<WorkCreatedEvent>
    {
        private readonly ProjectCache _projectCache;
        private readonly ProjectUserCache _projectUserCache;
        private readonly MessageBuffer _messageBuffer;
        private readonly IConnectionManager _connectionManager;
        public WorkIndexCommandHandler(ProjectUserCache projectUserCache, ProjectCache projectCache, MessageBuffer messageBuffer,IConnectionManager connection)
        {
            _projectCache = projectCache;
            _projectUserCache = projectUserCache;
            _messageBuffer = messageBuffer;
            _connectionManager= connection;
        }
        public async Task Handle(WorkCreatedEvent request, CancellationToken cancellationToken)
        {
            var UserId = request.comment.OrderByDescending(x => x.Timestamp).FirstOrDefault().UserId;
            try
            {
                //validate for the presence of both in it
                if (UserId != null && _projectCache.GetValue(request.ProjectId, out _) && _projectUserCache.GetValue(request.managerId, out _) && _projectUserCache.GetValue(request.assignedId, out _))
                {
                    ProjectProjection project = new();
                    bool resProject = _projectCache.GetValue(request.ProjectId, out project);
                    ProjectUserProjection projectAssignedUser = new();
                    ProjectUserProjection projectManager = new ProjectUserProjection();
                    bool resAssignedUser = _projectUserCache.GetValue(request.assignedId, out projectAssignedUser);
                    bool managerUser = _projectUserCache.GetValue(request.managerId, out projectManager);
                    List<Comments> comments = new();
                    List<CommentEventProperty> commentsInRequest = request.comment;
                    var commentsInIndexBody = commentsInRequest.ConvertAll<Comments>((x) =>
                    {
                        Comments comment = new();
                        comment.comment = x.comment;
                        comment.UserId = x.UserId;

                        //start of logic

                        ProjectUserProjection user = new();
                        bool res_bool = _projectUserCache.GetValue(x.UserId, out user);
                        if (res_bool)
                        {
                            comment.UserName = user.UserName;
                        }
                        else
                        {
                            comment.UserName = "Unknown User";
                        }
                        //this becomes a concern because all have to be validated, for now we can keep comments as unknown user for case where user was deleted
                        comment.Timestamp = x.Timestamp;
                        return comment;
                    }
                    );
                    WorkIndexBody body = new WorkIndexBody
                    {
                        Id = request.Id,
                        Name = request.Name,
                        ProjectId = request.ProjectId,
                        ProjectName = project.ProjectName,
                        _Comment = commentsInIndexBody,
                        assignedId = request.assignedId,
                        AssignedName = projectAssignedUser.UserName,
                        ManagerName = projectManager.UserName,
                        managerId = request.managerId,
                        description = request.description,
                        Deadline = request.deadline,
                        AssignmentDate = request.AssignmentDate,
                        WorkStatus = request.WorkStatus,
                        IsOverDue = request.IsOverDue,
                        Version = request.Version,
                    };
                    await _messageBuffer.AddMessage(body);

                }
                else
                {
                    //send his ass to cancun (Retry queue)
                    var connection = await _connectionManager.GetConnection();
                    using var channel = await connection.CreateChannelAsync();
                    var body_string = JsonConvert.SerializeObject(request);
                    byte[] body = UTF8Encoding.UTF8.GetBytes(body_string);
                    //just get the key in the way you were getting it
                    string routingkey = RoutingEventDirectory.GetRoutingKey(request.GetType());
                    await channel.BasicPublishAsync(
                        exchange: Topology.retryExchange,
                        routingKey: routingkey,
                        body: body,
                        cancellationToken: cancellationToken
                        );
                    return;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
}
}
