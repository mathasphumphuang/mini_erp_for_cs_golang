using CoreErpService.Models;
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
    }
}