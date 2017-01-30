using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myBills.web.Startup))]
namespace myBills.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
