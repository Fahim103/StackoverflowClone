using NHibernate;
using StackOverflow.Core.Entities;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        //public CommentRepository(ISession session) : base(session)
        //{

        //}
    }
}
