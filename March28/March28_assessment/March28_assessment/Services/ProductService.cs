using March28_assessment.Models;
using March28_assessment.Services;
using Microsoft.EntityFrameworkCore;

namespace March28_assessment.Services
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existing = await _context.Products.FindAsync(product.Id);
            if (existing == null) return null;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Category = product.Category;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}