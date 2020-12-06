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
        IQueryable<T> Get();
        IList<T> Get(Expression<Func<T, bool>> predicate = null);
        T Get(int id);
        void Create(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
