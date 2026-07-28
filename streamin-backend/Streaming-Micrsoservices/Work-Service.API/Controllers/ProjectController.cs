using MediatR;
using Microsoft.AspNetCore.Mvc;
using Work_Service.Application.ProjectContext;

namespace Work_Service.API.Controllers
{
    [ApiController]
    [Route("[Controller/action]")]
    public class ProjectController:Controller

    {
        private readonly IMediator mediator;
        public ProjectController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectCreateCommand command)
        {
            mediator.Send(command);
            return Ok("All good");
        }
        
    }
}
