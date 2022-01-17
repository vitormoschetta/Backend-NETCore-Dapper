using System;
using System.Data;

namespace Infra.Repositories
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IDbConnection connection)
        {            
            Connection = connection;
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}