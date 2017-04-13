using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quizmaker_Report_Suite.Startup))]
namespace Quizmaker_Report_Suite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
