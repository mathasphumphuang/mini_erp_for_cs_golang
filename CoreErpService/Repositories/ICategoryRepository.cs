using CoreErpService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreErpService.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
    }
}