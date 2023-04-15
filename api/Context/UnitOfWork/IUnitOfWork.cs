using api.Repositories;
using api.Repositories.UserRepository;

namespace api.Context.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }
        IUsersRepository UsersRepository { get; }

        void CommitTransaction();
        void Dispose();
        void RollbackTransaction();
    }
}