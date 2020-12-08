using NHibernate;
using StackOverflow.Core.Entities;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public class PostPointRepository : Repository<PostPoint>, IPostPointRepository
    {
        public PostPoint GetByUserId(ISession session, string userId)
        {
            return Get(session, x => x.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public (long upvote, long downvote, long overall) GetVotes(ISession session, int postId)
        {
            var query = session.Query<PostPoint>().Where(x => x.Post.Id == postId);
            var upvote = query.Where(x => x.IsUpvoted == true).Count();
            var downvote = query.Where(x => x.IsUpvoted == false).Count();
            var overall = upvote - downvote;

            return (upvote, downvote, overall);
        }
    }
}
