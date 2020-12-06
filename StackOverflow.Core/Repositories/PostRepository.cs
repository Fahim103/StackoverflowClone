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
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ISession session) : base(session)
        {

        }
    }
}
