using System;
using AutoMapper;
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
        public string PhotoPath { get; set; }
    }

    public class AddItemCommandHandler : RequestHandler<AddItemCommand>
    {
        private readonly IDatabase _database;

        public AddItemCommandHandler(IDatabase database)
        {
            _database = database;
        }

        protected override void HandleCore(AddItemCommand message)
        {
            var item = Mapper.Map<Models.Item>(message);

            _database.Insert(item);
        }
    }
}

