using System;
using Configuration = Homepwner.API.Constants.Configuration;
using System.IO;

namespace Homepwner.API.Services
{
    public interface IFileService
    {
        void AddFile(Guid fileId, byte[] file);
        void DeleteFile(Guid fileId);
        string GetFileSavePath(Guid fileId);
    }

    public class WindowsFileService : IFileService
    {
        private readonly ISystemTime _systemTime;
        private readonly Configuration _configuration;

        public WindowsFileService(ISystemTime systemTime, Configuration configuration)
        {
            _systemTime = systemTime;
            _configuration = configuration;
        }

        public void AddFile(Guid fileId, byte[] file)
        {
            var filePath = GetFileSavePath(fileId);

            var directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(filePath, file);
        }

        public void DeleteFile(Guid fileId)
        {
            var filePath = GetFileSavePath(fileId);
            File.Delete(filePath);
        }

        public string GetFileSavePath(Guid fileId)
        {
            return Path.Combine(_configuration.PhotoRootSavePath, _systemTime.Now.Date.ToString("yyyy-MM-dd"), fileId + ".png");
        }
    }
}