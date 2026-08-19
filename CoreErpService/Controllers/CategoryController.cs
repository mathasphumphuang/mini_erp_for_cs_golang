using CoreErpService.Models;
using CoreErpService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CoreErpService.DTOs.Category;

namespace CoreErpService.Controllers
{
    [Route("api/[controller]")] // กำหนด URL ให้เป็น /api/category
    [ApiController] // บอกระบบว่านี่คือ API นะ (จะช่วยตรวจสอบข้อมูลเบื้องต้นให้)
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            var response = categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return Ok(response);
        }

                [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(); // คืนค่า 404 ถ้าหาไม่เจอ
            }
            var response = new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var createdCategory = await _repository.AddAsync(category);
            
            return CreatedAtAction(nameof(GetAll), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {

            if (id != category.Id)
            {
                return BadRequest("ID ไม่ตรงกัน");
            }

            await _repository.UpdateAsync(category);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(); // คืนค่า 404 ถ้าหาไม่เจอ
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}