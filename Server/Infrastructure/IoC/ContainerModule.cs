using Autofac;
using Infrastructure.Config;
using Infrastructure.IoC.Modules;

namespace Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        //private readonly IContainer _container;

        //public ContainerModule(IContainer container)
        //{
        //    _container = container;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
            builder.RegisterModule<CommandsModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<RepositoryModule>();
        }
    }
}
