using System;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Commands
{
    public class ProductUpdateCommand : BaseProductCommand
    {
        public Guid Id { get; set; }

        public ValidationResult Validate()
        {
            return new ProductCommandValidator().Validate(this);
        }
    }
}