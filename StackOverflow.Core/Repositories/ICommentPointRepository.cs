using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public interface ICommentPointRepository : IRepository<CommentPoint>
    {
        CommentPoint GetByUserId(string userId);
        (long upvote, long downvote, long overall) GetVotes(int commentId);
    }
}
