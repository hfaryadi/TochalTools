using Microsoft.Owin;
using Owin;
using TochalTools.Models;

[assembly: OwinStartupAttribute(typeof(TochalTools.Startup))]
namespace TochalTools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            ConfigureAuth(app);
        }
    }
}
