using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestCam.Startup))]
namespace TestCam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
