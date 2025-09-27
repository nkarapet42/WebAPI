using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Infrastructure.Common;

namespace ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> Get(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductReadDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create([FromBody] ProductCreateDto dto)
        {
            var product = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}/name")]
        public async Task<ActionResult<ProductReadDto>> UpdateName(int id, [FromBody] string name)
        {
            var updated = await _service.UpdateNameAsync(id, name);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPut("{id}/categories")]
        public async Task<ActionResult<ProductReadDto>> UpdateCategories(int id, [FromBody] List<int> categoryIds)
        {
            var updated = await _service.UpdateCategoriesAsync(id, categoryIds);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPost("{id}/categories/{categoryId}")]
        public async Task<ActionResult<ProductReadDto>> AddCategory(int id, int categoryId)
        {
            var updated = await _service.AddCategoryAsync(id, categoryId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}/categories/{categoryId}")]
        public async Task<ActionResult<ProductReadDto>> RemoveCategory(int id, int categoryId)
        {
            var updated = await _service.RemoveCategoryAsync(id, categoryId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
