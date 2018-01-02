using System.Reflection;
using Autofac;
using Infrastructure.Commands;
using Infrastructure.Handlers;
using Module = Autofac.Module;

namespace Infrastructure.IoC.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandsModule).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IClientCommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICameraCommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CameraCommandDispatcher>()
                .As<ICameraCommandDispatcher>()
                .InstancePerLifetimeScope();

            builder.Register((c, p) => new ClientCommandDispatcher(c.Resolve<IComponentContext>(), p.Named<string>("ClientIP")))
                .As<IClientCommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
