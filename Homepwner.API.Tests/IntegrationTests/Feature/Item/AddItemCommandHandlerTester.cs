using System;
using System.IO;
using Autofac;
using Homepwner.API.Features.Item.Handlers;
using Homepwner.API.Features.Item.Models;
using Homepwner.API.Services;
using NUnit.Framework;
using Should;

namespace Homepwner.API.Tests.IntegrationTests.Feature.Item
{
    [TestFixture]
    public class AddItemCommandHandlerTester : MediatorTestFixtureBase
    {
        private IFileService _fileService;

        [OneTimeSetUp]
        public void Setup()
        {
            _fileService = Container.Resolve<IFileService>();
        }

        [Test]
        public void AddItemCommand_ShouldAddItem()
        {
            var addItemCommand = new AddItemCommand
            {
                Name = "Test1",
                SerialNumber = "123ASD",
                Value = 10,
                DateCreated = new DateTime(2015, 1, 1),
                Photo = new []{ (byte) 1},
            };

            Mediator.Send(addItemCommand);

            var item = Database.Single<Features.Item.Models.Item>("SELECT * FROM dbo.Item;");

            item.Name.ShouldEqual("Test1");
            item.SerialNumber.ShouldEqual("123ASD");
            item.Value.ShouldEqual(10);
            item.DateCreated.ShouldEqual(new DateTime(2015, 1, 1));

            var itemImage = Database.Single<ItemImage>("WHERE ItemId = @0;", item.Id);

            var file = _fileService.GetFileSavePath(itemImage.Id);

            File.Exists(file).ShouldBeTrue();
        }
    }
}