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
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            //builder.RegisterType<CommandDispatcher>()
            //    .As<ICommandDispatcher>()
            //    .InstancePerLifetimeScope();

            builder.Register((c, p) => new CommandDispatcher(c.Resolve<IComponentContext>(), p.Named<string>("ClientIP")))
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
