using System;
using System.Transactions;
using Autofac;
using AutoMapper;
using Homepwner.API.AutoMapper;
using Homepwner.API.Services;
using MediatR;
using Moq;
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
        public IMediator Mediator { get; private set; }
        public IDatabase Database { get; private set; }
        public Mock<IFileService> FileServiceMock { get; }

        public MediatorTestFixtureBase()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DomainIocModule());

            FileServiceMock = new Mock<IFileService>();
            FileServiceMock.Setup(mock => mock.AddFile(It.IsAny<Guid>(), It.IsAny<byte[]>()));
            builder.Register(cfg => FileServiceMock.Object).As<IFileService>();

            var container = builder.Build();

            Mediator = container.Resolve<IMediator>();
            Database = container.Resolve<IDatabase>();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        }
    }
}
