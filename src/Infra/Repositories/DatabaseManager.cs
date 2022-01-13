using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Infra.Repositories
{
    public abstract class DatabaseManager
    {
        public IDbTransaction _transaction { get; set; }
        public IDbConnection _connection { get; set; }

        public DatabaseManager(IDbConnection connection)
        {
            _connection = connection;
        }


        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }


        /// <summary>
        /// Fecha a conexão se estiver aberta e se não houver uma transação iniciada
        /// </summary>
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed && _transaction == null)
            {
                _connection.Close();
            }
        }


        public IDbCommand CreateCommand(string commandText)
        {
            var command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            command.Transaction = _transaction;
            return command;
        }


        /// <summary>
        /// Executa um comando SQL (Insert, Update ou Delete)
        /// </summary>
        public int Execute(string commandText)
        {
            OpenConnection();
            var command = CreateCommand(commandText);
            var response = command.ExecuteNonQuery();
            CloseConnection();
            return response;
        }


        /// <summary>
        /// Executa uma consulta SQL que retorna uma única linha
        /// </summary>
        public TResponse QuerySingle<TResponse>(string commandText)
        {
            OpenConnection();
            var response = _connection.Query<TResponse>(commandText).FirstOrDefault();
            CloseConnection();
            return response;
        }


        /// <summary>
        /// Executa uma consulta SQL que retorna multiplas linhas
        /// </summary>
        public IList<TResponse> QueryList<TResponse>(string commandText)
        {
            OpenConnection();
            var response = _connection.Query<TResponse>(commandText).ToList();
            CloseConnection();
            return response;
        }


        /// <summary>
        /// Executa uma consulta SQL que retorna apenas um registro (uma coluna)
        /// </summary>
        public object QueryScalar(string commandText)
        {
            OpenConnection();
            var command = CreateCommand(commandText);
            var response = command.ExecuteScalar();
            CloseConnection();
            return response;
        }


        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                OpenConnection();
                _transaction = _connection.BeginTransaction();
            }
        }


        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("A transação já foi comitada");
            }

            _transaction.Commit();
            Dispose();
        }


        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                Dispose();
            }
        }


        public void Dispose()
        {            
            _connection.Close();
            GC.SuppressFinalize(this);
        }
    }
}