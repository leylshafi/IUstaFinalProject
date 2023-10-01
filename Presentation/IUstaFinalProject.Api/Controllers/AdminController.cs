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

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryReadRepository productReadRepository;
        private readonly ICategoryWriteRepository productWriteRepository;

        private readonly IMediator mediator;
        private readonly ILogger<AdminController> logger;

        public AdminController(ICategoryReadRepository productReadRepository, ICategoryWriteRepository productWriteRepository, IMediator mediator, ILogger<AdminController> logger)
        {
            this.productReadRepository = productReadRepository;
            this.productWriteRepository = productWriteRepository;
            this.mediator = mediator;
            this.logger = logger;
        }


        [HttpPost("add category")]
        public async Task<IActionResult> Add([FromBody] AddCategoryCommandRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mediator.Send(request);
                    return StatusCode((int)HttpStatusCode.Created);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveCategoryCommandRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await mediator.Send(request);
                    return StatusCode((int)HttpStatusCode.Created);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        //[HttpGet("statistics")]
        //public IActionResult GetStatistics()
        //{
        //    try
        //    {
        //        var customerCount = workerDbContext.Customers.Count();
        //        var workerCount = workerDbContext.Workers.Count();
        //        var categoryCount = workerDbContext.Categories.Count();

        //        var statistics = new
        //        {
        //            CustomerCount = customerCount,
        //            WorkerCount = workerCount,
        //            CategoryCount = categoryCount
        //        };

        //        return Ok(statistics);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
