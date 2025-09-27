using ProductCatalog.Application.DTOs;
using ProductCatalog.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryReadDto?> GetByIdAsync(int id);
        Task<PagedResult<CategoryReadDto>> GetPagedAsync(int page, int pageSize);
        Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
        Task<CategoryReadDto?> UpdateAsync(int id, CategoryCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
