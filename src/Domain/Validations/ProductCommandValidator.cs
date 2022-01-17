using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
    public class ProductCommandValidator : AbstractValidator<BaseProductCommand>
    {
        public ProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}