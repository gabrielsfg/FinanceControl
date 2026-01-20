using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Services.Services;
using FinanceControl.Shared.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubCategoryController : BaseController
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategoryAsnc([FromBody] CreateSubCategoryRequestDto requestDto)
        {
            var userId = GetUserId();

            var result = await _subCategoryService.CreateSubCategoryAsync(requestDto, userId);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllSubCategoryAsync()
        {
            var userId = GetUserId();

            var result = await _subCategoryService.GetAllSubCategoryAsync(userId);
            return Ok(result);
        }

        [HttpGet("by-id/{id:int}")]
        public async Task<IActionResult> GetSubCategoryByIdAsync([FromRoute]int id)
        {
            var userId = GetUserId();

            var result = await _subCategoryService.GetSubCategoryByIdAsync(id, userId);

            if (result == null)
                return NotFound("SubCategory not found.");

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSubCategoryAsync([FromBody]UpdateSubCategoryRequestDto requestDto)
        {
            var userId = GetUserId();

            var result = await _subCategoryService.UpdateSubCategoryAsync(requestDto, userId);

            if (result.IsFailure)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSubCategoryAsync([FromRoute] int id)
        {
            var userId = GetUserId();
            var result = await _subCategoryService.DeleteSubCategoryAsync(id, userId);

            if (result.IsFailure)
                return NotFound(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}
