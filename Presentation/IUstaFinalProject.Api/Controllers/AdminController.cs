using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IUstaFinalProject.Application.Features.Commands.Categories.AddCategory;
using MediatR;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Application.Features.Commands.Categories.RemoveCategory;
using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Infrastructure.Services;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILoginRegisterService _loginRegister;

        public AdminController(IUnitOfWork unit, ILoginRegisterService loginRegister)
        {
            this.unit = unit;
            this._loginRegister = loginRegister;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto admin)
        {
            try
            {
                var token = _loginRegister.Login(admin, Role.Admin);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto admin)
        {
            try
            {
                if (await _loginRegister.Register(admin, Role.Admin, null))
                    return Ok();
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetWorkers")]
        public IActionResult Get()
        {
            try
            {
                List<Worker> workers = unit.WorkerReadRepository.GetAll().ToList();
                if (workers is not null)
                    return Ok(workers);
                else
                    return BadRequest("Worker does not exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
            try
            {
                var customerCount = unit.CustomerReadRepository.GetAll().Count();
                var workerCount = unit.WorkerReadRepository.GetAll().Count();
                var categoryCount = unit.CategoryReadRepository.GetAll().Count();

                var statistics = new
                {
                    CustomerCount = customerCount,
                    WorkerCount = workerCount,
                    CategoryCount = categoryCount
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
