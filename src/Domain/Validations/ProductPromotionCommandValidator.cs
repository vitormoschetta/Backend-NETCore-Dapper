using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
    public class ProductPromotionCommandValidator: AbstractValidator<ProductPromotionCommand>
    {
        public ProductPromotionCommandValidator()
        {            
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}