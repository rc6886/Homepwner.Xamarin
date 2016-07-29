using Autofac;
using AutoMapper;
using Homepwner.API.AutoMapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.TinyIoc;

namespace Homepwner.API
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new IocModule());

            builder.Update(existingContainer.ComponentRegistry);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }
    }
}