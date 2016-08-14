using System;
using Homepwner.API.Services;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class GetItemImageQuery : IRequest<byte[]>
    {
        public Guid ItemId { get; set; }
    }

    public class GetItemImageQueryHandler : IRequestHandler<GetItemImageQuery, byte[]>
    {
        private readonly IDatabase _database;
        private readonly IFileService _fileService;

        public GetItemImageQueryHandler(IDatabase database, IFileService fileService)
        {
            _database = database;
            _fileService = fileService;
        }

        public byte[] Handle(GetItemImageQuery message)
        {
            using (_database)
            {
                var imageId = _database.ExecuteScalar<Guid>("SELECT Id FROM dbo.ItemImage WHERE ItemId = @0;", message.ItemId);
                return _fileService.GetFileAsBytes(imageId);
            }
        }
    }
}