using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public interface ICommentPointService
    {
        void Update(CommentPoint commentPoint);
        void Create(CommentPoint commentPoint);
        int GetCount(int commentId);
        CommentPoint GetByUserId(string userId);
        (long upvote, long downvote, long overall) GetVotes(int postId);
    }
}
