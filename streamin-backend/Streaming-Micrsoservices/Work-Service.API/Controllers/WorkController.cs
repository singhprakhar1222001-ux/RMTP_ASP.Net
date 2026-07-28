using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_Service.Application.Abstractions;
using Work_Service.Application.ProjectContext;
using Work_Service.Application.WorkContext;
using Work_Service.Domain.ProjectContext;
using Work_Service.Domain.ProjectUser;
using Work_Service.Domain.WorkContext;
using WorkService.Persistance.UnitOfWork;

namespace Work_Service.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class WorkController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitofWork<Workitem> _unitOfWork;
        
        public WorkController(IMediator mediator,IUnitofWork<Workitem> unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
           
        }
        [HttpGet]
        public IActionResult GetWork()
        {
            return Ok("No work");
        }

        [Route("CreateWork")]
        [HttpPost]
        public async  Task<IActionResult> CreateWork([FromBody] WorkCreateCommand request)
        {
            await _mediator.Send(request);
            return Ok("request Processed");
        }

        //[Route("CreateProject")]
        //[HttpPost]
        //public async Task<IActionResult> CreateProject([FromBody] ProjectCreateCommand request)
        //{
        //    await _mediator.Send(request);
        //    return Ok("request Processed");
        //}
        [Route("AddComment")]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromRoute] string comment,Guid WorkId)
        {
            var work = await _unitOfWork.GetEntity(WorkId);
            work.AddComment(comment);
            return Ok();
        }
        [Route("ChangeAssignee")]
        [HttpPost]
        public async Task<IActionResult> ChangeAssignment([FromRoute] Guid newUserId, Guid WorkId)
        {

            var workitem = await _unitOfWork.GetEntity(WorkId);
            workitem.ChangeAssignee(newUserId);
            return Ok("Assignee was changed");
        }
        [Route("ChangeDeadline")]
        [HttpPost]
        public async Task<IActionResult> ChangeAssignment([FromRoute] DateOnly newDate,Guid WorkId)
        {
            var workItem = await _unitOfWork.GetEntity(WorkId);
            workItem.ChangeDeadline(newDate);
            return Ok("Deadline was changed");
        }



    }
    }
