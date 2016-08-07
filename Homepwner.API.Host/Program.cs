using System;
using Microsoft.Owin.Hosting;

namespace Homepwner.API.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "http://localhost:1986";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}...", url);
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
