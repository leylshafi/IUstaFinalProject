using IUstaFinalProject.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Create user")]
        public async Task <IActionResult> Create(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response= await  mediator.Send(request);
            return Ok(response);
        }
    }
}
