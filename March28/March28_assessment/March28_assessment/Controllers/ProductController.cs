using March28_assessment.Models;
using March28_assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace March28_assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _service;

        public ProductController(IProduct service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var result = await _service.AddAsync(product);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var result = await _service.UpdateAsync(product);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
