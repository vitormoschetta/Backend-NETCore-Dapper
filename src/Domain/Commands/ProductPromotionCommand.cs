using System;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Commands
{
    public class ProductPromotionCommand
    {
        public Guid Id { get; set; }        
        public decimal Price { get; set; }

        public ValidationResult Validate()
        {
            return new ProductPromotionCommandValidator().Validate(this);
        }
    }
}