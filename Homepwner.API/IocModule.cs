using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using Homepwner.API.Features.Item.Handlers;
using MediatR;
using NPoco;
using Module = Autofac.Module;

namespace Homepwner.API
{
    public class IocModule : Module
    {
        private readonly string _databaseConnectionString = ConfigurationManager.ConnectionStrings["Homepwner"].ConnectionString;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(AddItemCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterType<Mediator>().As<IMediator>();
            builder.Register(c => new Database(_databaseConnectionString, DatabaseType.SqlServer2012))
                .As<IDatabase>();
        }
    }
}