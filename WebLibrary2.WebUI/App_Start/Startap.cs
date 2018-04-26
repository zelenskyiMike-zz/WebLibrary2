using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using WebLibrary2.BLL.Interfaces;
using WebLibrary2.BLL.Sevices;
using Microsoft.AspNet.Identity;

namespace WebLibrary2.WebUI.App_Start
{
    public class Startap
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("EFDbContext");
        }
    }
}