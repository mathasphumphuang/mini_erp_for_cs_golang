using CoreErpService.Models;

namespace CoreErpService.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId);
        Task<Product> AddAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}