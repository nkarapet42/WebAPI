using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure.Interfaces;
using ProductCatalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Categories)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Products
                                 .Include(p => p.Categories)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
