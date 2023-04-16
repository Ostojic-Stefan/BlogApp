using System.Data;
using api.Repositories.PostsRepository;
using api.Repositories.UserRepository;
using api.Services.Account;
using Microsoft.Data.Sqlite;

namespace api.Context.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;
        public IPostsRepository PostsRepository { get; private set; }
        public IUsersRepository UsersRepository { get; private set; }

        public UnitOfWork(IConfiguration configuration, IPasswordService passwordService)
        {
            _connection = new SqliteConnection(configuration.GetConnectionString("SqlConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            PostsRepository = new PostsRepository(_connection);
            UsersRepository = new UsersRepository(_connection, passwordService);
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