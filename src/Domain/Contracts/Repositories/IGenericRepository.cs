using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> : IDisposable
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(Guid id);
        Task Add(TEntity item);
        Task Update(TEntity item);
        Task Delete(TEntity item);
        Task Delete(Guid id);
    }
}