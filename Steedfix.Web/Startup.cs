using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Steedfix.Web.Startup))]
namespace Steedfix.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
