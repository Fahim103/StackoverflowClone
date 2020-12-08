using StackOverflow.Core.UnitOfWorks;
using System.Linq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StackOverflow.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public virtual void Create(ISession session, T entity)
        {
            session.Save(entity);             
        }

        public virtual void Update(ISession session, T entity)
        {
            session.Update(entity);
        }

        public virtual void Delete(ISession session, int id)
        {
            Delete(session, Get(session, id));
        }

        public virtual void Delete(ISession session, T entity)
        {
            session.Delete(entity);           
        }

        public virtual T Get(ISession session, int id)
        {
            return session.Get<T>(id);
        }

        public virtual IList<T> Get(ISession session)
        {
            return session.Query<T>().ToList();
        }

        public virtual int GetCount(ISession session, Expression<Func<T, bool>> predicate = null)
        {
            var count = session.Query<T>()
                .Where(predicate)
                .Count();

            return count;
        }

        public virtual IList<T> Get(ISession session, Expression<Func<T, bool>> predicate = null)
        {
            var list = session.Query<T>()
                .Where(predicate)
                .ToList();

            return list;
        }
    }
}
