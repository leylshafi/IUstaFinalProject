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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILoginRegisterService _loginRegister;
        public CustomerController(ILoginRegisterService loginRegister, IUnitOfWork unit)
        {
            this._loginRegister = loginRegister;
            this.unit = unit;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] CustomerDto customer,Role role)
        {
            try
            {
                var token = _loginRegister.Login(customer,role);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerDto customer,Role role)
        {
            try
            {
                if (await _loginRegister.Register(customer,role))
                    return Ok();
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
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
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetWorkerbyCategory")]
        public async Task<IActionResult> GetWorkerByProfession([FromBody] CategoryDto category)
        {
            try
            {
                Worker worker = await unit.WorkerReadRepository.GetSingleAsync(u => u.Category.CategoryName == category.CategoryName);
                if (worker is not null)
                    return Ok(worker);
                else
                {
                    List<Category> professions = unit.CategoryReadRepository.GetAll().ToList();
                    return BadRequest($"The Profession was not found!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
