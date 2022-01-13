using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetByFilter(string filter);
        Task<bool> Exists(string name);
        Task<bool> ExistsUpdate(string name, Guid id);
        
    }
}