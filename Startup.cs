using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PHEDServe.Startup))]
namespace PHEDServe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
