using IUstaFinalProject.Domain.Entities.Dtos;
using IUstaFinalProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Application.Enums;
using IUstaFinalProject.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILoginRegisterService _loginRegister;
        private readonly ILogger<AdminController> logger;
        private readonly IMemoryCache _memoryCache;
        public AdminController(IUnitOfWork unit, ILoginRegisterService loginRegister, ILogger<AdminController> logger, IMemoryCache memoryCache)
        {
            this.unit = unit;
            this._loginRegister = loginRegister;
            this.logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto admin)
        {
            try
            {
                var token = _loginRegister.Login(admin, Role.Admin);
                logger.LogInformation("Admin logined");
                return Ok(token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
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

                logger.LogInformation("Admin registered");
                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetWorkers")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                if (_memoryCache.TryGetValue("Workers", out IEnumerable<Worker> cachedWorkers))
                {
                    return Ok(cachedWorkers);
                }

                List<Worker> workers = unit.WorkerReadRepository.GetAll().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) 
                };

                _memoryCache.Set("Workers", workers, cacheEntryOptions);

                if (workers != null && workers.Count > 0)
                {
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


        [HttpGet("statistics")]
        [Authorize(Roles = "Admin")]
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
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
