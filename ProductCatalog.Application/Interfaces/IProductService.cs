using ProductCatalog.Application.DTOs;
using ProductCatalog.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadDto?> GetByIdAsync(int id);
        Task<PagedResult<ProductReadDto>> GetPagedAsync(int page, int pageSize);
        Task<ProductReadDto> CreateAsync(ProductCreateDto dto);
        Task<ProductReadDto?> UpdateNameAsync(int id, string newName);
        Task<ProductReadDto?> AddCategoryAsync(int productId, int categoryId);
        Task<ProductReadDto?> RemoveCategoryAsync(int productId, int categoryId);
        Task<ProductReadDto?> UpdateCategoriesAsync(int productId, List<int> categoryIds);
        Task<bool> DeleteAsync(int id);
    }
}