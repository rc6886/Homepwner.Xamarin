using System;
using System.IO;
using Homepwner.API.Services;
using Moq;
using NUnit.Framework;
using Should;
using Configuration = Homepwner.API.Constants.Configuration;

namespace Homepwner.API.Tests.IntegrationTests.Services
{
    public class WindowsFileServiceTester
    {
        private IFileService _fileService;
        private Configuration _configuration;

        [OneTimeSetUp]
        public void Setup()
        {
            var systemTimeMock = new Mock<ISystemTime>();
            systemTimeMock.Setup(mock => mock.Now).Returns(new DateTime(2015, 1, 1));
            var systemTime = systemTimeMock.Object;

            _configuration = new Configuration();

            _fileService = new WindowsFileService(systemTime, _configuration);
        }

        [Test]
        public void AddFile_ShouldAddFileToFileSystem()
        {
            var fileId = AddFile();
            var filePath = _fileService.GetFileSavePath(fileId);
            File.Exists(filePath).ShouldBeTrue();
        }

        [Test]
        public void DeleteFile_ShouldDeleteFileFromFileSystem()
        {
            var fileId = AddFile();
            var filePath = _fileService.GetFileSavePath(fileId);

            _fileService.DeleteFile(fileId);

            File.Exists(filePath).ShouldBeFalse();
        }

        [Test]
        public void GetFileSavePath_ShouldReturnCorrectPath()
        {
            var fileId = Guid.NewGuid();
            var fileSavePath = _fileService.GetFileSavePath(fileId);
            fileSavePath.ShouldEqual(Path.Combine(@"C:\Homepwner\Test\Photos\2015-01-01", fileId + ".png"));
        }

        private Guid AddFile()
        {
            var file = new[] { (byte)1, (byte)1 };
            var fileId = Guid.NewGuid();

            _fileService.AddFile(fileId, file);

            return fileId;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            var files = Directory.GetFiles(_configuration.PhotoRootSavePath);
            var dirs = Directory.GetDirectories(_configuration.PhotoRootSavePath);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                Directory.Delete(dir, true);
            }

            Directory.Delete(_configuration.PhotoRootSavePath, true);
        }
    }
}