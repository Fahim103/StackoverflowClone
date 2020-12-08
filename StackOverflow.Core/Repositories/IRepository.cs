using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> Get(ISession session);
        IList<T> Get(ISession session, Expression<Func<T, bool>> predicate = null);
        int GetCount(ISession session, Expression<Func<T, bool>> predicate = null);
        T Get(ISession session, int id);
        void Create(ISession session, T entity);
        void Update(ISession session, T entity);
        void Delete(ISession session, T entity);
        void Delete(ISession session, int id);
    }
}
