using System;

namespace Homepwner.API.Services
{
    public interface ISystemTime
    {
        DateTime Now { get; }
    }

    public class SystemTime : ISystemTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}