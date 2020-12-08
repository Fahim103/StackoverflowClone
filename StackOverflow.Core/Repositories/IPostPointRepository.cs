using NHibernate;
using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public interface IPostPointRepository : IRepository<PostPoint>
    {
        (long upvote, long downvote, long overall) GetVotes(ISession session, int postId);
        PostPoint GetByUserId(ISession session, string userId);
    }
}
