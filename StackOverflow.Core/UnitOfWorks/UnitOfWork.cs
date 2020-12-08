using NHibernate;
using StackOverflow.Core.Repositories;
using System;

namespace StackOverflow.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ITransaction _transaction;
        public IPostRepository PostRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IPostPointRepository PostPointRepository { get; set; }
        public ICommentPointRepository CommentPointRepository { get; set; }


        public UnitOfWork(IPostRepository postRepository, ICommentRepository commentRepository,
            IPostPointRepository postPointRepository, ICommentPointRepository commentPointRepository)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            PostPointRepository = postPointRepository;
            CommentPointRepository = commentPointRepository;
        }

        public void BeginTransaction(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Commit();
                }
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                }
            }
        }

        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }   
        }

        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}
