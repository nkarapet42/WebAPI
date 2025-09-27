using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure.Common;
using ProductCatalog.Infrastructure.Interfaces;

namespace ProductCatalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<PagedResult<CategoryReadDto>> GetPagedAsync(int page, int pageSize)
        {
            var items = await _categoryRepository.GetPagedAsync(page, pageSize);
            var total = await _categoryRepository.CountAsync();

            return new PagedResult<CategoryReadDto>
            {
                Items = _mapper.Map<IEnumerable<CategoryReadDto>>(items),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category(dto.Name);
            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto?> UpdateAsync(int id, CategoryCreateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            category.UpdateName(dto.Name);
            await _categoryRepository.UpdateAsync(category);

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepository.DeleteAsync(category);
            return true;
        }
    }
}

