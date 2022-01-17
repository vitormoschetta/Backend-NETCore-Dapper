using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infra.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<bool> Exists(string name)
        {
            var exist = false;

            var query = "select * from product p where p.name = @Name";
            var response = await _connection.QueryAsync<Product>(query, new { Name = name });

            if (response.Any()) exist = true;

            return exist;
        }

        public async Task<bool> ExistsUpdate(string name, Guid id)
        {
            var exist = false;

            var query = "select * from product p where p.name = @Name and p.id != @Id";
            var response = await _connection.QueryAsync<Product>(query, new { Name = name, Id = id });

            if (response.Any()) exist = true;

            return exist;
        }

        public async Task<IEnumerable<Product>> GetByFilter(string filter)
        {
            var query = $"select * from product p where p.name like '%{filter}%'";
            return await _connection.QueryAsync<Product>(query);
        }
    }
}