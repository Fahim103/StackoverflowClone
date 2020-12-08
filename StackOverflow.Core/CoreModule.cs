using Autofac;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Repositories;
using StackOverflow.Core.Services;
using StackOverflow.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentPointRepository>().As<ICommentPointRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostPointRepository>().As<IPostPointRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentPointService>().As<ICommentPointService>().InstancePerLifetimeScope();
            builder.RegisterType<PostPointService>().As<IPostPointService>().InstancePerLifetimeScope();


            builder.Register(s => NHibernateDbContext.GetSession()).As<ISession>().InstancePerLifetimeScope();
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();
            builder.RegisterType<UserManager<ApplicationUser>>();

            base.Load(builder);
        }
    }
}
