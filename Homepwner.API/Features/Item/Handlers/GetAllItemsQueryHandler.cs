using System.Collections.Generic;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class GetAllItemsQuery : IRequest<IEnumerable<Models.Item>>
    {
    }

    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Models.Item>>
    {
        private readonly IDatabase _database;

        public GetAllItemsQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<Models.Item> Handle(GetAllItemsQuery message)
        {
            using (_database)
            {
                return _database.Fetch<Models.Item>("ORDER BY DateCreated;");
            }
        }
    }
}