using AutoMapper;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BLL.Mapping;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractUnitOfWork;
using WebLibrary2.Domain.Concrete.ConcreteAuthor;
using WebLibrary2.Domain.Concrete.ConcreteUnitOfWork;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IAuthorsRepository>().To<EFAuthorRepository>();
            Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();
        }

        private IMapper AutoMapper(Ninject.Activation.IContext context)
        {

            Mapper.Initialize(config =>
            {
                MappingProfile.InitializeAutoMapper();
            });

            Mapper.AssertConfigurationIsValid();
            return Mapper.Instance;
        }

    }
}
