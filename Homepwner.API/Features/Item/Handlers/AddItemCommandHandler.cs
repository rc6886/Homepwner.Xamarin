using System;
using AutoMapper;
using MediatR;

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
        public AddItemCommandHandler()
        {
        }

        protected override void HandleCore(AddItemCommand message)
        {
            var item = Mapper.Map<Models.Item>(message);
        }
    }
}

