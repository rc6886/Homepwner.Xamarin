using System;
using MediatR;

namespace Homepwner.API
{
    public class AddItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public double Value { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class AddItemCommandHandler : RequestHandler<AddItemCommand>
    {
        public AddItemCommandHandler()
        {
        }

        protected override void HandleCore(AddItemCommand message)
        {
            throw new NotImplementedException();
        }
    }
}

