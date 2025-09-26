using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure.Data;
using ProductCatalog.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Categories
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
