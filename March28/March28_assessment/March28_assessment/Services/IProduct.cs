using March28_assessment.Models;

namespace March28_assessment.Services
{
    public interface IProduct
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<Product?> DeleteAsync(int id);
    }
}