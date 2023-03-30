using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(masterpeace2.Startup))]
namespace masterpeace2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
