using NHibernate;
using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public class CommentPointRepository : Repository<CommentPoint>, ICommentPointRepository
    {
        public CommentPoint GetByUserId(string userId)
        {
            return Get(x => x.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public (long upvote, long downvote, long overall) GetVotes(int commentId)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {

                var query = _session.Query<CommentPoint>().Where(x => x.Comment.Id == commentId);
                var upvote = query.Where(x => x.IsUpvoted == true).Count();
                var downvote = query.Where(x => x.IsUpvoted == false).Count();
                var overall = upvote - downvote;

                return (upvote, downvote, overall);
            }
        }
    }
}
