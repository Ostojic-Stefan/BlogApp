using api.Repositories;

namespace api.Context.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }

        void CommitTransaction();
        void Dispose();
        void RollbackTransaction();
    }
}