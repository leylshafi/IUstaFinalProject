using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILoginRegisterService _loginRegister;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(IUnitOfWork unit, ILoginRegisterService loginRegister, ILogger<WorkerController> logger)
        {
            this.unit = unit;
            this._loginRegister = loginRegister;
            _logger = logger;
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
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto workerDto)
        {
            try
            {
                var token = _loginRegister.Login(workerDto,Role.Worker);
                _logger.LogInformation("Worker logined");
                return Ok("Successfully Logined!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto workerDto,string categoryId)
        {
            try
            {

                if (await _loginRegister.Register(workerDto, Role.Worker, categoryId))
                {
                    _logger.LogInformation("Worker registered");
                    return Ok();
                }

                
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile(Guid workerId)
        {
            try
            {
                var worker = await unit.WorkerReadRepository.GetSingleAsync(c => c.Id == workerId);
                if (worker == null) return NotFound("Worker not found!");

                var profile = new
                {
                    name = worker.Name,
                    surname = worker.Surname,
                    username = worker.Username
                };

                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("Get Customers")]
        public IActionResult GetCustomers()
        {
            try
            {
                List<Customer> customers = unit.CustomerReadRepository.GetAll().ToList();
                if (customers is not null)
                    return Ok(customers);
                else
                    return BadRequest("Customer does not exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
