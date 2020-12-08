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
        public virtual void Create(T entity)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Save(entity);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted)
                        {
                            _transaction.Rollback();
                        }

                        throw new Exception("Failed to insert : " + ex.Message);
                    }
                }
            }
        }

        public virtual void Update(T entity)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Update(entity);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted)
                        {
                            _transaction.Rollback();
                        }

                        throw new Exception("Faild to update : " + ex.Message);
                    }
                }

            }
        }

        public virtual void Delete(int id)
        {
            Delete(Get(id));
        }

        public virtual void Delete(T entity)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Delete(entity);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted)
                        {
                            _transaction.Rollback();
                        }

                        throw new Exception("Faild to delete : " + ex.Message);
                    }
                }
            }
        }

        public virtual T Get(int id)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {
                return _session.Get<T>(id);
            }
        }

        public virtual IList<T> Get()
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {

                return _session.Query<T>().ToList();
            }
        }

        public virtual int GetCount(Expression<Func<T, bool>> predicate = null)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {

                var count = _session.Query<T>()
                    .Where(predicate)
                    .Count();

                return count;
            }
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            using (ISession _session = NHibernateDbContext.GetSession())
            {

                var list = _session.Query<T>()
                    .Where(predicate)
                    .ToList();

                return list;
            }
        }
    }
}
