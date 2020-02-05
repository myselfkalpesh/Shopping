using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shopping.Web.Startup))]
namespace Shopping.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
