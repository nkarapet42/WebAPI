using FluentValidation;
using ProductCatalog.Application.DTOs;

namespace ProductCatalog.Application.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required");

            RuleFor(x => x.CategoryIds)
                .NotNull().WithMessage("Categories required")
                .Must(x => x.Count == 2 || x.Count == 3)
                .WithMessage("Product must have 2 or 3 categories")
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("Categories must be unique");
        }
    }
}