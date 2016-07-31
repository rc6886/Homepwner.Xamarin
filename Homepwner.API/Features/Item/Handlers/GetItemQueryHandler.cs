using System;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class GetItemQuery : IRequest<Models.Item>
    {
        public Guid Id { get; set; } 
    }

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, Models.Item>
    {
        private readonly IDatabase _database;

        public GetItemQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public Models.Item Handle(GetItemQuery message)
        {
            using (_database)
            {
                return _database.Single<Models.Item>("WHERE Id = @0;", message.Id);
            }
        }
    }
}