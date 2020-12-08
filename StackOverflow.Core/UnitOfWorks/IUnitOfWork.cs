using NHibernate;
using StackOverflow.Core.Repositories;

namespace StackOverflow.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void BeginTransaction(ISession session);
        void Commit();
        void Rollback();
        IPostRepository PostRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IPostPointRepository PostPointRepository { get; set; }
        ICommentPointRepository CommentPointRepository { get; set; }
    }
}
