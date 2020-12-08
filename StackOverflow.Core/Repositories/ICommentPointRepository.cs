using NHibernate;
using StackOverflow.Core.Entities;

namespace StackOverflow.Core.Repositories
{
    public interface ICommentPointRepository : IRepository<CommentPoint>
    {
        CommentPoint GetByUserId(ISession session, string userId);
        (long upvote, long downvote, long overall) GetVotes(ISession session, int commentId);
    }
}
