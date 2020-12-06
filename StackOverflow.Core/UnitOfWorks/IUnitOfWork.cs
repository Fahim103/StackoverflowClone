using StackOverflow.Core.Repositories;

namespace StackOverflow.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IPostRepository PostRepository { get; set; }
    }
}
