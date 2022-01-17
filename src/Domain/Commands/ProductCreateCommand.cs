using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Commands
{
    public class ProductCreateCommand : BaseProductCommand
    {
        public ValidationResult Validate()
        {
            return new ProductCommandValidator().Validate(this);
        }
    }
}