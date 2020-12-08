using NHibernate;
using StackOverflow.Core.Entities;

namespace StackOverflow.Core.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

    }
}
