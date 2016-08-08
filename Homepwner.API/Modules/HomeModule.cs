﻿using System.Collections.Generic;
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

            Post["/items"] = parameters => GetAllItems();
            Post["/item/update"] = parameters =>
            {
                var updateItemCommand = this.Bind<UpdateItemCommand>();
                UpdateItem(updateItemCommand);
                return HttpStatusCode.OK;
            };
        }

        private IEnumerable<Item> GetAllItems()
        {
            return _mediator.Send(new GetAllItemsQuery());
        }

        private void UpdateItem(UpdateItemCommand command)
        {
            _mediator.Send(command);
        }
    }
}

