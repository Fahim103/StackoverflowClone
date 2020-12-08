using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Services
{
    public interface IPostPointService
    {
        void Update(PostPoint postPoint);
        void Create(PostPoint postPoint);
        PostPoint GetByPostAndUserId(int postId, string userId);
        int GetCount(Expression<Func<PostPoint, bool>> predicate);
        (long upvote, long downvote, long overall) GetVotes(int postId);
    }
}
