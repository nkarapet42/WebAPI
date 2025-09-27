using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

            CreateMap<ProductCreateDto, Product>()
                .ConstructUsing(dto => new Product(dto.Name, new List<Category>()));
        }
    }
}
