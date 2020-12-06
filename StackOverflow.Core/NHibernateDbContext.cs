using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Tool.hbm2ddl;
using StackOverflow.Core.Convention;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Mappings;

namespace StackOverflow.Core
{
    public class NHibernateDbContext
    {
        private static ISessionFactory _session;

        private static ISessionFactory CreateSession()
        {
            if (_session != null)
            {
                return _session;
            }

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
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostPointMappings>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CommentPointMappings>())
                .ExposeConfiguration(cfg =>
                {
                    cfg.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);
                    new SchemaUpdate(cfg).Execute(false, true);
                });

            _session = _config.BuildSessionFactory();

            return _session;
        }

        public static ISession GetSession()
        {
            return CreateSession().OpenSession();
        }
    }
}
