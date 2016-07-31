using System;
using Homepwner.API.Services;
using MediatR;
using NPoco;

namespace Homepwner.API.Features.Item.Handlers
{
    public class DeleteItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
    }

    public class DeleteItemCommandHandler : RequestHandler<DeleteItemCommand>
    {
        private readonly IDatabase _database;
        private readonly IFileService _fileService;

        public DeleteItemCommandHandler(IDatabase database, IFileService fileService)
        {
            _database = database;
            _fileService = fileService;
        }

        protected override void HandleCore(DeleteItemCommand message)
        {
            using (_database)
            {
                using (var transaction = _database.GetTransaction())
                {
                    _database.Delete<Models.Item>("WHERE Id = @0;", message.Id);
                    _fileService.DeleteFile(message.FileId);

                    transaction.Complete();
                }
            }
        }
    }
}