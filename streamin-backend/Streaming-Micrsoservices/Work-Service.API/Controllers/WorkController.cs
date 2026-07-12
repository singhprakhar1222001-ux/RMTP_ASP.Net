using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_Service.Application.ProjectContext;
using Work_Service.Application.WorkContext;

namespace Work_Service.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class WorkController : Controller
    {
        private readonly IMediator _mediator;
        public WorkController(IMediator mediator)
        {
            _mediator = mediator;
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

        [Route("CreateProject")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectUserCommand request)
        {
            await _mediator.Send(request);
            return Ok("request Processed");
        }
    }
}
