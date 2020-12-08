using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Serilog;
using StackOverflow.Core;
using StackOverflow.Core.Entities;
using StackOverflow.Core.Seed;
using StackOverflow.Core.Services;
using System.Web.Hosting;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(StackOverflow.Web.Startup))]
namespace StackOverflow.Web
{
    public partial class Startup
    {
        public static ILifetimeScope AutofacContainer { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new CoreModule());

            builder.Register<ILogger>((componentContext, parameters) =>
            {
                var test = componentContext;
                var param = parameters;
                var logPath = HostingEnvironment.MapPath("~/Logs/log-.txt");
                return new LoggerConfiguration()
                    .Enrich.FromLogContext().WriteTo
                    .File(logPath, rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}")
                    .CreateLogger();
            }).SingleInstance();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AutofacContainer = container;

            ConfigureAuth(app);

            // Seed Roles
            new ApplicationUserRoleSeed().GenerateUserRoles();
            // Seed Admin
            new ApplicationUserSeed(AutofacContainer.Resolve<UserManager<ApplicationUser>>()).SeedUser();
        }
    }
}
