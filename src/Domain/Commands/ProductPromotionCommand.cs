using System;

namespace Domain.Commands
{
    public class ProductPromotionCommand
    {
        public Guid Id { get; set; }        
        public decimal Price { get; set; }
    }
}