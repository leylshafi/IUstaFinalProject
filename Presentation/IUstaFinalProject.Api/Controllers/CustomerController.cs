using IUstaFinalProject.Application.Abstraction.Services;
using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
        private readonly IMailService mailService; 
        private readonly IMemoryCache _memoryCache;
        public CustomerController(ILoginRegisterService loginRegister, IUnitOfWork unit, ILogger<CustomerController> logger, IMailService mailService, IMemoryCache memoryCache)
        {
            this._loginRegister = loginRegister;
            this.unit = unit;
            this.logger = logger;
            this.mailService = mailService;
            _memoryCache = memoryCache;
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

        [HttpGet("Get all Workers")]
        public IActionResult Get()
        {
            try
            {
                if (_memoryCache.TryGetValue("AllWorkers", out IEnumerable<Worker> cachedWorkers))
                {
                    return Ok(cachedWorkers);
                }
                List<Worker> workers = unit.WorkerReadRepository.GetAll().ToList();

                if (workers != null && workers.Count > 0)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    };

                    _memoryCache.Set("AllWorkers", workers, cacheEntryOptions);

                    workers = workers.OrderByDescending(w => w.Rating).ToList();

                    return Ok(workers);
                }
                else
                {
                    return BadRequest("No workers found.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpGet("RateWorker")]
        public async Task<IActionResult> RateWorker(Guid WorkerId, float rating)
        {
            try
            {
                Worker worker = await unit.WorkerReadRepository.GetSingleAsync(w => w.Id == WorkerId);
                if (worker == null)
                {
                    return BadRequest("Worker does not exist");
                }

                worker.RatedCustomers += 1;
                worker.Rating = (worker.Rating + rating) / worker.RatedCustomers; 

                unit.WorkerWriteRepository.Update(worker);
                await unit.WorkerWriteRepository.SaveAsync();

                return Ok(worker);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Agreement with worker")]
        public async Task<IActionResult> AgreementWithWorker(Guid WorkerId, Guid CustomerId, string AgreementText)
        {
            try
            {
                Agreement agreement = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    CustomerId = CustomerId,
                    WorkerId = WorkerId,
                    AgreementText = AgreementText
                };
                await unit.AgreementWriteRepository.AddAsync(agreement);
                await unit.AgreementWriteRepository.SaveAsync();
                
                var worker = await unit.WorkerReadRepository.GetSingleAsync(w=>w.Id== WorkerId);
                var customer = await unit.CustomerReadRepository.GetSingleAsync(w=>w.Id== CustomerId);
                await mailService.SendMailAsync(worker.Email, $"Agreement with {customer.Name}", AgreementText, true);

                return Ok(agreement);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }



    }
}
