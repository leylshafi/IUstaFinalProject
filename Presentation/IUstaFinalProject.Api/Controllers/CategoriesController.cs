using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Domain.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IUstaFinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<CategoriesController> logger;
        public CategoriesController(IUnitOfWork unit, ILogger<CategoriesController> logger)
        {
            this.unit = unit;
            this.logger = logger;
        }

        [HttpGet("Get Categories")]
        public IActionResult Get()
        {
            try
            {
                List<Category> categories = unit.CategoryReadRepository.GetAll().ToList();
                if (categories is not null)
                    return Ok(categories);
                else
                    return BadRequest("Category does not exists");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("Add Category")]
        public async Task<IActionResult> Add([FromBody] CategoryDto categoryDto)
        {
            try
            {
                Category category = new()
                {
                    Id = Guid.NewGuid(),
                    CategoryName = categoryDto.CategoryName
                };

                var result = await unit.CategoryWriteRepository.AddAsync(category);
                if (result)
                {
                    await unit.SaveChangesAsync();
                    return Ok();
                }
                else
                    return BadRequest("Something get wrong!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateCategoryByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] CategoryDto categoryDto, string newName)
        {
            try
            {
                var category = await unit.CategoryReadRepository.GetSingleAsync(p => p.CategoryName == categoryDto.CategoryName);
                if (category == null)
                    return NotFound();

                category.CategoryName = newName;

                unit.CategoryWriteRepository.Update(category);
                await unit.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteCategoryByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var profession = await unit.CategoryReadRepository.GetSingleAsync(p => p.CategoryName == categoryDto.CategoryName);
                if (profession == null)
                    return NotFound();

                unit.CategoryWriteRepository.Remove(profession);
                await unit.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

