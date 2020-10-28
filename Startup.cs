using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeniusWebApp.Startup))]
namespace GeniusWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
