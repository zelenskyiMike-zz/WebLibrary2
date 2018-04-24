using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebLibrary2.BLL.Infrastructure;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string connectionString;
        protected void Application_Start()
        {
            //Database.SetInitializer(new Configuration());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new ServiceModule(connectionString);
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
