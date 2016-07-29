using System;
using MediatR;
using Nancy;

namespace Homepwner.API.Extensions
{
    public static class ExtensionsOfRouteBuilder
    {
        public static NancyModule.RouteBuilder Post<T>(this NancyModule nancyModule, Func<T, dynamic> actionMethod) where T : IRequest
        {
            return null;
        }
    }
}