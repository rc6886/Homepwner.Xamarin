using System;
using Homepwner.API.Features.Item.Handlers;
using Moq;
using NUnit.Framework;
using Should;

namespace Homepwner.API.Tests.IntegrationTests.Feature.Item
{
    [TestFixture]
    public class AddItemCommandHandlerTester : MediatorTestFixtureBase
    {
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

            FileServiceMock.Verify(mock => mock.AddFile(It.IsAny<Guid>(), It.IsAny<byte[]>()), Times.Once);
        }
    }
}