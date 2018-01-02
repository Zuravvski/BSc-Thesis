using Autofac;
using Infrastructure.Config;
using Infrastructure.IoC.Modules;

namespace Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
            builder.RegisterModule<CommandsModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<RepositoryModule>();
        }
    }
}
