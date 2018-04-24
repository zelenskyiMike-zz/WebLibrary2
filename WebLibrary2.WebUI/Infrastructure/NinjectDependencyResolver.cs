using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using Moq;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Concrete.ConcreteBook;
using WebLibrary2.Domain.Concrete.ConcreteAuthor;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Concrete.ConcreteArticle;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Concrete.ConcreteMagazine;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Concrete.ConcretePublication;
using WebLibrary2.Domain.Abstract.AbstractUnitOfWork;
using WebLibrary2.Domain.Concrete.ConcreteUnitOfWork;

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
            kernel.Bind<IAuthorsRepository>().To<EFAuthorRepository>();

            kernel.Bind<IBookRepository>().To<EFBookRepository>();

            kernel.Bind<IBookAuthorsRepository>().To<EFBookAuthorRepository>();

            kernel.Bind<IArticleRepository>().To<EFArticleRepository>();
            kernel.Bind<IArticeAuthorsRepository>().To<EFArticleAuthorsRepository>();

            kernel.Bind<IMagazineRepository>().To<EFMagazineRepository>();
            kernel.Bind<IMagazineAuthorsRepository>().To<EFMagazineAuthorRepository>();

            kernel.Bind<IPublicationRepository>().To<EFPublicationRepository>();
            kernel.Bind<IPublicationAuthorsRepository>().To<EFPublicationAuthorsRepository>();

            //kernel.Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}