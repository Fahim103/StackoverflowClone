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
        //private UnitOfWork _unitOfWork;
        //public Repository(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = (UnitOfWork)unitOfWork;
        //}

        public Repository(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; private set; }

        public IQueryable<T> Get()
        {
            return Session.Query<T>();
        }

        public IList<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return Session.Query<T>().Where(predicate).ToList();
        }

        public T Get(int id)
        {
            return Session.Get<T>(id);
        }

        public void Create(T entity)
        {
            Session.Save(entity);
        }

        public void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(int id)
        {
            //Session.Delete(Session.Load<T>(id));
            var entity = Get(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}
