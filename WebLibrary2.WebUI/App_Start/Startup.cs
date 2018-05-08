using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.BusinessLogicLayer.Interfaces;

[assembly: OwinStartup(typeof(WebLibrary2.WebUI.App_Start.Startup))]

namespace WebLibrary2.WebUI.App_Start
{
    public class Startup
    {
        ServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }
    }

}
