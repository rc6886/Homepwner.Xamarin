using System;
using AutoMapper;
using Homepwner.API.Features.Item.Models;
using Homepwner.API.Services;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class AddItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public double Value { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Photo { get; set; }
    }

    public class AddItemCommandHandler : RequestHandler<AddItemCommand>
    {
        private readonly IDatabase _database;
        private readonly IFileService _fileService;

        public AddItemCommandHandler(IDatabase database, IFileService fileService)
        {
            _database = database;
            _fileService = fileService;
        }

        protected override void HandleCore(AddItemCommand message)
        {
            using (_database)
            {
                using (var transaction = _database.GetTransaction())
                {
                    var item = Mapper.Map<Models.Item>(message);
                    item.Id = Guid.NewGuid();

                    _database.Insert(item);

                    var itemImage = new ItemImage
                    {
                        Id = Guid.NewGuid(),
                        ItemId = item.Id,
                        DateCreated = DateTime.UtcNow,
                    };

                    _database.Insert(itemImage);

                    _fileService.AddFile(itemImage.Id, message.Photo);

                    transaction.Complete();
                }
            }
        }
    }
}

