using Microsoft.AspNet.Identity;
using LectioService;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LectioServer.Startup))]

namespace LectioServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            LectioService.Constants.LoadConstants(new LectioContext());
        }
    }
}