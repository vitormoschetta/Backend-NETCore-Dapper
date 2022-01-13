using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Domain.Contracts.Repositories;

namespace Infra.Repositories
{
    public abstract class GenericRepository<TEntity> : DatabaseManager, IGenericRepository<TEntity> where TEntity : class
    {
        protected GenericRepository(System.Data.IDbConnection connection) : base(connection)
        {
        }

        public async Task Add(TEntity item)
        {
            await _connection.InsertAsync<TEntity>(item);
        }

        public async Task Delete(TEntity item)
        {
            await _connection.DeleteAsync<TEntity>(item);
        }

        public async Task Delete(Guid id)
        {
            var item = await _connection.GetAsync<TEntity>(id);
            await _connection.DeleteAsync<TEntity>(item);
        }

        public async Task Update(TEntity item)
        {
            await _connection.UpdateAsync<TEntity>(item);
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await _connection.GetAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _connection.GetAllAsync<TEntity>();
        }
    }
}