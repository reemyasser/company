using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(company.Startup))]
namespace company
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
