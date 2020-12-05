using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackOverflow.Web.Startup))]
namespace StackOverflow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
