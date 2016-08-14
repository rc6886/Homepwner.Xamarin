using System;
using System.Collections.Generic;
using System.IO;
using Homepwner.API.Features.Item.Handlers;
using Homepwner.API.Features.Item.Models;
using MediatR;
using Nancy;
using Nancy.ModelBinding;

namespace Homepwner.API.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IMediator _mediator;

        public HomeModule(IMediator mediator)
        {
            _mediator = mediator;

            Get["/"] = parameters => "The Homepwner API is running...";
            Get["/item/getimage/{itemId}"] = parameters =>
            {
                var itemId = (Guid)parameters.itemId;
                return GetItemImage(itemId);
            };

            Post["/items"] = parameters => GetAllItems();
            Post["/item/update"] = parameters =>
            {
                var updateItemCommand = this.Bind<UpdateItemCommand>();
                UpdateItem(updateItemCommand);
                return HttpStatusCode.OK;
            };
            Post["/item/delete"] = parameters =>
            {
                var deleteItemCommand = this.Bind<DeleteItemCommand>();
                DeleteItem(deleteItemCommand);
                return HttpStatusCode.OK;
            };
        }

        private Response GetItemImage(Guid itemId)
        {
            const string contentType = "image/png";
            var image = _mediator.Send(new GetItemImageQuery {ItemId = itemId});
            var stream = new MemoryStream(image);

            return Response.FromStream(stream, contentType);
        }

        private IEnumerable<Item> GetAllItems()
        {
            return _mediator.Send(new GetAllItemsQuery());
        }

        private void UpdateItem(UpdateItemCommand command)
        {
            _mediator.Send(command);
        }

        private void DeleteItem(DeleteItemCommand deleteItemCommand)
        {
            _mediator.Send(deleteItemCommand);
        }
    }
}

