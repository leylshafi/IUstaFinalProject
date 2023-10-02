using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILoginRegisterService _loginRegister;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ILoginRegisterService loginRegister, IUnitOfWork unit, ILogger<CustomerController> logger)
        {
            this._loginRegister = loginRegister;
            this.unit = unit;
            this.logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto customer)
        {
            try
            {
                var token = _loginRegister.Login(customer,Role.Customer);
                logger.LogInformation("Customer logined");
                return Ok(token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto customer)
        {
            try
            {
                if (await _loginRegister.Register(customer,Role.Customer,null))
                    return Ok();

                logger.LogInformation("Customer registered");
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile(Guid workerId)
        {
            try
            {
                var customer = await unit.CustomerReadRepository.GetSingleAsync(c => c.Id == workerId);
                if (customer == null) return NotFound("Customer not found!");

                var profile = new
                {
                    name = customer.Name,
                    surname = customer.Surname,
                    username = customer.Username
                };

                return Ok(profile);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetWorkerbyCategory/{category}")]
        public async Task<IActionResult> GetWorkerByProfession(string category)
        {
            try
            {
                Category categoryFind = await unit.CategoryReadRepository.GetSingleAsync(c => c.CategoryName == category);

                if (categoryFind != null)
                {
                    Worker worker = await unit.WorkerReadRepository.GetSingleAsync(u => u.CategoryId == categoryFind.Id);

                    if (worker != null)
                        return Ok(worker);
                    else
                        return BadRequest($"No worker found for the given category!");
                }
                else
                {
                    return BadRequest($"The category was not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("Get All Workers")]
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
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
