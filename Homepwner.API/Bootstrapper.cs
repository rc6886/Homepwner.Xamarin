using Autofac;
using AutoMapper;
using Homepwner.API.AutoMapper;
using Nancy;
using Nancy.Bootstrappers.Autofac;

namespace Homepwner.API
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DomainIocModule());

            builder.Update(container.ComponentRegistry);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }
    }
}