using CoreErpService.Models;
using CoreErpService.Interfaces;
using CoreErpService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreErpService.Controllers
{
    [Route("api/[controller]")] // กำหนด URL ให้เป็น /api/category
    [ApiController] // บอกระบบว่านี่คือ API นะ (จะช่วยตรวจสอบข้อมูลเบื้องต้นให้)
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        // รับ Repository (พ่อครัว) เข้ามาทำงาน
        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // Endpoint 1: ดึงข้อมูลหมวดหมู่ทั้งหมด
        // รองรับคำสั่ง HTTP GET -> /api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories); // ส่งรหัส 200 (OK) พร้อมกับข้อมูลกลับไป
        }

        // Endpoint 2: สร้างหมวดหมู่ใหม่
        // รองรับคำสั่ง HTTP POST -> /api/category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            // ถ้าข้อมูลที่ส่งมาไม่ถูกต้อง (เช่น ผิดชนิดข้อมูล)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _repository.AddAsync(category);
            
            // ส่งรหัส 201 (Created) กลับไป พร้อมข้อมูลที่ถูกสร้างสำเร็จ
            return CreatedAtAction(nameof(GetAll), new { id = createdCategory.Id }, createdCategory);
        }
        // Endpoint 3: ดึงข้อมูลตาม ID
        // GET: /api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(); // คืนค่า 404 ถ้าหาไม่เจอ
            }
            return Ok(category);
        }

        // Endpoint 4: แก้ไขข้อมูล
        // PUT: /api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            // เช็คว่า ID ใน URL กับใน ข้อมูลที่ส่งมาตรงกันไหม (เพื่อความปลอดภัย)
            if (id != category.Id)
            {
                return BadRequest("ID ไม่ตรงกัน"); // คืนค่า 400
            }

            await _repository.UpdateAsync(category);
            return NoContent(); // คืนค่า 204 (ทำสำเร็จแต่ไม่มีข้อมูลจะส่งกลับ)
        }

        // Endpoint 5: ลบข้อมูล
        // DELETE: /api/category/5
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