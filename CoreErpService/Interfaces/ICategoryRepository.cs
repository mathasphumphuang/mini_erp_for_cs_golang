using CoreErpService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreErpService.Interfaces
{
    public interface ICategoryRepository
    {
        // Ienumberable คือการบอกว่าเราจะส่งกลับข้อมูลหลายรายการ (List) ของ Category
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}