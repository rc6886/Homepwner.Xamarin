using System;
using Homepwner.API.Features.Item.Handlers;
using NUnit.Framework;
using Should;

namespace Homepwner.API.Tests.IntegrationTests.Feature.Item
{
    [TestFixture]
    public class AddItemCommandHandlerTester : TestFixtureBase
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
                PhotoPath = "PhotoPath",
            };

            Mediator.Send(addItemCommand);

            var item = Database.Single<Features.Item.Models.Item>("SELECT * FROM dbo.Item;");

            item.Name.ShouldEqual("Test1");
            item.SerialNumber.ShouldEqual("123ASD");
            item.Value.ShouldEqual(10);
            item.DateCreated.ShouldEqual(new DateTime(2015, 1, 1));
        }
    }
}