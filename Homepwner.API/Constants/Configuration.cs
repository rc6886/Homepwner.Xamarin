using System.Configuration;

namespace Homepwner.API.Constants
{
    public class Configuration
    {
        public string PhotoRootSavePath => ConfigurationManager.AppSettings["PhotoRootSavePath"];
    }
}