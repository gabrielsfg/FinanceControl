using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody]CreateCategoryRequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest();

            var userId = GetUserId();

            var result = await _categoryService.CreateCategoryAsync(requestDto, userId);

            return Created($"/api/categories/{result.Value!.Id}", result.Value);

        }
    }
}
