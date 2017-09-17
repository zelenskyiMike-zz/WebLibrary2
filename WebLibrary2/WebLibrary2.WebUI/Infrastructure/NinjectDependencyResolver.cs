using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using Moq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Concrete;

namespace WebLibrary2.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return (kernel.TryGet(serviceType));
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (kernel.GetAll(serviceType));
        }

        private void AddBindings()
        {
              kernel.Bind<IBookRepository>().To<EFBookRepository>();
        }
    }
}