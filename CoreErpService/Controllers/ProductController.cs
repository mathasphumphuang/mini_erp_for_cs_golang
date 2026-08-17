using CoreErpService.Models;
using CoreErpService.Interfaces;
using CoreErpService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreErpService.Controllers
{
    [Route("api/[controller]")] 
    [ApiController] 
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetAllByCategory(int id)
        {
            var products = await _repository.GetAllByCategoryIdAsync(id);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProduct = await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetAll), new { id = createdProduct.Id }, createdProduct);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID ไม่ตรงกัน");
            }
            await _repository.UpdateAsync(product);
            return NoContent();
        }
        [HttpDelete()]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}