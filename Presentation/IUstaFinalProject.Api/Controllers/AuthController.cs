using IUstaFinalProject.Application.Abstraction.Services;
using IUstaFinalProject.Application.Features.Commands.AppUser.LoginUser;
using IUstaFinalProject.Application.Features.Commands.AppUser.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService mailService;
        readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, IMailService mailService, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            this.mailService = mailService;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            await mailService.SendMailAsync("llshafi03@gmail.com", "Test", "<h1>This is a test mail</h1>");
            _logger.LogInformation("Email Sent");
            return Ok(response);
        }

        [HttpPost("RefreshTokenLogin")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }

        //[HttpPost("password-reset")]
        //public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        //{
        //    PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
        //    return Ok(response);
        //}

        //[HttpPost("verify-reset-token")]
        //public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        //{
        //    VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
        //    return Ok(response);
        //}
    }
}
