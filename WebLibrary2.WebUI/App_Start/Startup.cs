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
using System.ComponentModel;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Identity;
using WebLibrary2.Domain.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(WebLibrary2.WebUI.App_Start.Startup))]

namespace WebLibrary2.WebUI.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
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
