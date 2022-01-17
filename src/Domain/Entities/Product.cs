using System;
using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
        }

        public Product(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }
        
        [ExplicitKey]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }


        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void AddPromotion(decimal price)
        {
            if (Price < price)
            {
                throw new ArgumentException("Preço promocional maior que preço normal. ");
            }

            Price = price;
        }
    }
}