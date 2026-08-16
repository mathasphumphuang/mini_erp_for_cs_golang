using CoreErpService.Data;
using CoreErpService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreErpService.Repositories
{
    // สังเกตตรง : ICategoryRepository คือการประกาศว่า Class นี้ขอเซ็นสัญญารับทำงานตามที่ Interface กำหนด
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // รับ DbContext เข้ามาเพื่อใช้คุยกับ Database
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. ฟังก์ชันดึงข้อมูลทั้งหมด
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            // ไปดึงตาราง Categories ทั้งหมดออกมาเป็น List แบบ Async
            return await _context.Categories.ToListAsync();
        }

        // 2. ฟังก์ชันเพิ่มข้อมูลใหม่
        public async Task<Category> AddAsync(Category category)
        {
            // เอาข้อมูลใหม่ไปต่อคิวเตรียมบันทึก
            _context.Categories.Add(category);
            
            // สั่งยืนยันการบันทึกลง Database จริงๆ (คล้ายๆ การกด Commit)
            await _context.SaveChangesAsync();
            
            return category;
        }
    }
}