using System.Transactions;
using Autofac;
using AutoMapper;
using Homepwner.API.AutoMapper;
using MediatR;
using NPoco;
using NUnit.Framework;

namespace Homepwner.API.Tests
{
    public class TestFixtureBase
    {
        private TransactionScope _transactionScope;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            _transactionScope = new TransactionScope();
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            _transactionScope?.Dispose();
        }
    }

    public class MediatorTestFixtureBase : TestFixtureBase
    {
        public IContainer Container { get; }
        public IMediator Mediator { get; private set; }
        public IDatabase Database { get; private set; }

        public MediatorTestFixtureBase()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DomainIocModule());

            Container = builder.Build();

            Mediator = Container.Resolve<IMediator>();
            Database = Container.Resolve<IDatabase>();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }
    }
}
