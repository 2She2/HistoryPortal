using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HistoryPortal.Web.Startup))]
namespace HistoryPortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
