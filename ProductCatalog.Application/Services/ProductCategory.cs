using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure.Common;
using ProductCatalog.Infrastructure.Interfaces;

namespace ProductCatalog.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var categories = new List<Category>();
            foreach (var catId in dto.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(catId);
                if (category != null) categories.Add(category);
            }

            var product = new Product(dto.Name, categories);
            await _productRepository.AddAsync(product);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductReadDto>(product);
        }

        public async Task<PagedResult<ProductReadDto>> GetPagedAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetPagedAsync(page, pageSize);
            var count = await _productRepository.CountAsync();

            var dtos = _mapper.Map<List<ProductReadDto>>(products);
            return new PagedResult<ProductReadDto>(dtos, count, page, pageSize);
        }

        public async Task<ProductReadDto?> UpdateNameAsync(int id, string newName)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            product.UpdateName(newName);
            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto?> AddCategoryAsync(int productId, int categoryId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (product == null || category == null) return null;

            product.AddCategory(category);
            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto?> RemoveCategoryAsync(int productId, int categoryId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (product == null || category == null) return null;

            product.RemoveCategory(category);
            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto?> UpdateCategoriesAsync(int productId, List<int> categoryIds)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return null;

            var categories = new List<Category>();
            foreach (var catId in categoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(catId);
                if (category != null) categories.Add(category);
            }

            product.UpdateCategories(categories);
            await _productRepository.UpdateAsync(product);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}

