using System;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class UpdateItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public double Value { get; set; }
        public Guid ImageFileId { get; set; }
        public byte[] Image { get; set; }
    }

    public class UpdateItemCommandHandler : RequestHandler<UpdateItemCommand>
    {
        private readonly IDatabase _database;

        public UpdateItemCommandHandler(IDatabase database)
        {
            _database = database;
        }

        protected override void HandleCore(UpdateItemCommand message)
        {
            using (_database)
            {
                var item = _database.Single<Models.Item>("WHERE Id = @0;", message.Id);

                item.Name = message.Name;
                item.SerialNumber = message.SerialNumber;
                item.Value = message.Value;

                _database.Update(item);
            }
        }
    }
}