using System;
using Homepwner.API.Services;
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
        private readonly IFileService _fileService;

        public GetItemQueryHandler(IDatabase database, IFileService fileService)
        {
            _database = database;
            _fileService = fileService;
        }

        public Models.Item Handle(GetItemQuery message)
        {
            using (_database)
            {
                return _database.Single<Models.Item>(@"SELECT * FROM dbo.Item WHERE Id = @0;", message.Id);
            }
        }
    }
}