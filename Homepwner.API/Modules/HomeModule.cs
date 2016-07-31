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

            Get["/"] = parameters => "Hello world";
        }
    }
}

