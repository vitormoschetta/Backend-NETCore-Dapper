using System;
using System.Collections.Generic;
using System.Data;

namespace Domain.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }     
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}