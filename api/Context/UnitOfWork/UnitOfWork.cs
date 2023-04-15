using System.Data;
using api.Repositories;
using Microsoft.Data.Sqlite;

namespace api.Context.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;
        public IPostsRepository PostsRepository { get; private set; }

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqliteConnection(configuration.GetConnectionString("SqlConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            PostsRepository = new PostsRepository(_connection);
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}