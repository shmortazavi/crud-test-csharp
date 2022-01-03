using Autofac;
using Mc2.CrudTest.Application.Commands;
using Mc2.CrudTest.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mc2.CrudTest.Application
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();
          
            builder.RegisterType<Mc2DbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(UpdateCustomerCommand).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(DeleteCustomerCommand).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                  .Where(x => x.Namespace.StartsWith("M2c.CrudTest.Infrastructure"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
