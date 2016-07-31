using Homepwner.API.Features.Item.Handlers;
using MediatR;
using Nancy;

namespace Homepwner.API.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IMediator _mediator;

        public HomeModule(IMediator mediator)
        {
            _mediator = mediator;
            _mediator.Send(new AddItemCommand());

            Get["/"] = parameters => "Hello world";
        }
    }
}

