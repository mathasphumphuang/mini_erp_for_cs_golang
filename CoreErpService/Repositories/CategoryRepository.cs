using CoreErpService.Data;
using CoreErpService.Models;
using CoreErpService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreErpService.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // รับ DbContext เข้ามาเพื่อใช้คุยกับ Database
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync(); // ToListAsync() คือการดึงข้อมูลทั้งหมดจากตาราง Categories และแปลงเป็น List ของ Category
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category); // Add(...) คือการบอกให้ EF Core เตรียมเพิ่มข้อมูลใหม่ลงในตาราง Categories
            
            await _context.SaveChangesAsync(); // SaveChangesAsync() คือการบอกให้ EF Core ส่งคำสั่ง SQL ไปยัง Database เพื่อบันทึกการเปลี่ยนแปลงจริง ๆ
            
            return category;
        }
        // ดึงหมวดหมู่ตาม ID
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                                 .Include(c => c.Products)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category); // Remove(...) คือการบอกให้ EF Core เตรียมลบข้อมูลออกจากตาราง Categories
                await _context.SaveChangesAsync();
            }
        }
    }
}