using AutoMapper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebLibrary2.BusinessLogicLayer.Infrastructure;
using WebLibrary2.BusinessLogicLayer.Mapping;
using WebLibrary2.BusinessLogicLayer.Mapping.MappingProfiles;

namespace WebLibrary2.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string connectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new ServiceModule(connectionString);
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            MappingProfile.InitializeAutoMapper();

        }
    }
}
