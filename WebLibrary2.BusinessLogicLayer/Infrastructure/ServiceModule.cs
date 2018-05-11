using AutoMapper;
using Ninject.Modules;
using WebLibrary2.BusinessLogicLayer.Mapping;

namespace WebLibrary2.BusinessLogicLayer.Infrastructure
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
            //Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
           // Bind<IAuthorsRepository>().To<EFAuthorRepository>();
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
