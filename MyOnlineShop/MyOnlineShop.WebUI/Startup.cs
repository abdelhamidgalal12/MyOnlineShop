using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyOnlineShop.WebUI.Startup))]
namespace MyOnlineShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
