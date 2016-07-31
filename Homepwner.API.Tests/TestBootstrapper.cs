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

        public IMediator Mediator { get; private set; }
        public IDatabase Database { get; private set; }

        public TestFixtureBase()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new IocModule());
            var container = builder.Build();

            Mediator = container.Resolve<IMediator>();
            Database = container.Resolve<IDatabase>();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }

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
}
