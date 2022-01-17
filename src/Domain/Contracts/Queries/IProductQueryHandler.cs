using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts.Queries
{
    public interface IProductQueryHandler
    {
        Task<IEnumerable<Product>> Handle();
        Task<IEnumerable<Product>> Handle(string filter);
        Task<Product> Handle(Guid id);
    }
}