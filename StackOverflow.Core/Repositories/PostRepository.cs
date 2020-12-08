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
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public override Post Get(int id)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {
                var entity = _session.Get<Post>(id);
                NHibernateUtil.Initialize(entity.Comments);
                NHibernateUtil.Initialize(entity.ApplicationUser);

                return entity;
            }
        }
    }
}
