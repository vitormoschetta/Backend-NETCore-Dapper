using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Domain.Contracts.Repositories;

namespace Infra.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbSession _session;

        protected GenericRepository(DbSession session)
        {
            _session = session;
        }

        public async Task Add(TEntity item)
        {
            await _session.Connection.InsertAsync<TEntity>(item, _session.Transaction);
        }

        public async Task Delete(TEntity item)
        {
            await _session.Connection.DeleteAsync<TEntity>(item, _session.Transaction);
        }

        public async Task Delete(Guid id)
        {
            var item = await _session.Connection.GetAsync<TEntity>(id, _session.Transaction);
            await _session.Connection.DeleteAsync<TEntity>(item, _session.Transaction);
        }

        public async Task Update(TEntity item)
        {
            await _session.Connection.UpdateAsync<TEntity>(item, _session.Transaction);
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await _session.Connection.GetAsync<TEntity>(id, _session.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _session.Connection.GetAllAsync<TEntity>(_session.Transaction);
        }
    }
}