using System;

namespace Domain.Queries.Responses
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            
        }
        public ProductResponse(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}