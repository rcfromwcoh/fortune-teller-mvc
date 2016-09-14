using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FortuneTellerMVC.Startup))]
namespace FortuneTellerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
