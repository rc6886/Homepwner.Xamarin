using Nancy;
namespace Homepwner.API
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = parameters => "Hello world";
        }
    }
}

