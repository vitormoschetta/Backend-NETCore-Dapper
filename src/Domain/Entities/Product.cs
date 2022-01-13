using System;
using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    [Table("Product")]
    public class Product: Notifiable
    {
        public Product()
        {            
        }
        public Product(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;

            Validate();
        }     
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }        


        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;

            Validate();
        }

        public void AddPromotion(decimal price)
        {
            if (Price < price)
            {
                AddNotification("Preço promocional maior que preço normal. ");
                return;
            }

            Price = price;            
        }


        public void Validate()
        {
            if (string.IsNullOrEmpty(Name)) {
                AddNotification("Nome do Produto é obrigatório! ");
                return;
            }
                
            if (Name.Length < 4)
                AddNotification("Nome do Produto deve conter no mínimo 4 caracteres. ");

            if (Price <= 0)
                AddNotification("Preço do Produto informado é inválido. ");
        }
    }
}