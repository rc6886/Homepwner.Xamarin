using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using Homepwner.API.AutoMapper;
using Homepwner.API.Features.Item.Handlers;
using MediatR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.TinyIoc;

namespace Homepwner.API
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var builder = new ContainerBuilder();

            //builder.RegisterModule(new IocModule());

            //builder.Build();


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

            builder.Update(container.ComponentRegistry);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            //base.ConfigureApplicationContainer(container);

            //var builder = new ContainerBuilder();

            ////builder.RegisterModule(new IocModule());

            ////builder.Build();


            //builder.RegisterSource(new ContravariantRegistrationSource());
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(AddItemCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();
            //builder.Register<SingleInstanceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});
            //builder.Register<MultiInstanceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            //});
            //builder.RegisterType<Mediator>().As<IMediator>();

            //builder.Update(container.ComponentRegistry);

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<DefaultMappingProfile>();
            //});
        }
    }
}