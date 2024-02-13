using FluentValidation;
using WebShop.Service.Dtos;

namespace WebShop.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Product name can't be empty");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description can't be empty");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Product price can't be empty or zero")
                .GreaterThan(0)
                .WithMessage("Product price can't be empty or zero");

            RuleFor(x => x.StockQuantity).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
