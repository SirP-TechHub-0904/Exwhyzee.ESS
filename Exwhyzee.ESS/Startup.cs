using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exwhyzee.ESS.Startup))]
namespace Exwhyzee.ESS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
