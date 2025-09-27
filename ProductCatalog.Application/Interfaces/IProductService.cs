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
        Task<ProductReadDto?> UpdateAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
