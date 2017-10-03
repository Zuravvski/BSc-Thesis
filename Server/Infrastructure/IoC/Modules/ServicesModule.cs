using System.Reflection;
using Autofac;
using Infrastructure.Services;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServicesModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
