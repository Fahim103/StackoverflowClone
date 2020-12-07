using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Tool.hbm2ddl;
using StackOverflow.Core.Convention;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Mappings;
using StackOverflow.Core.Repositories;

namespace StackOverflow.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;
        public ISession Session { get; set; }
        public IPostRepository PostRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IPostPointRepository PostPointRepository { get; set; }
        public ICommentPointRepository CommentPointRepository { get; set; }

        static UnitOfWork()
        {
            var myEntities = new[] {
                typeof(ApplicationUser)
            };

            FluentConfiguration _config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x.Server(@".\SQLEXPRESS")
                .Username("demo")
                .Password("123456")
                .Database("StackOverflowClone")))
                .Mappings(m => m.FluentMappings.Conventions.AddFromAssemblyOf<TableNameConvention>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMappings>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CommentMappings>())
                .ExposeConfiguration(cfg =>
                {
                    cfg.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);
                    new SchemaUpdate(cfg).Execute(false, true);
                });

            _sessionFactory = _config.BuildSessionFactory();
        }

        public UnitOfWork(IPostRepository postRepository, ICommentRepository commentRepository,
            IPostPointRepository postPointRepository, ICommentPointRepository commentPointRepository)
        {
            Session = _sessionFactory.OpenSession();
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            PostPointRepository = postPointRepository;
            CommentPointRepository = commentPointRepository;
        }

        public void BeginTransaction()
        {
            if (!Session.IsOpen)
            {
                Session = _sessionFactory.OpenSession();
            }
            _transaction = Session.BeginTransaction();
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
            finally
            {
                Session.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                }
            }
            finally
            {
                Session.Dispose();
            }
        }
    }
}
