using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mPOS.WebAPI.Startup))]
namespace mPOS.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
