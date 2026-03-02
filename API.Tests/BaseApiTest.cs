using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Infrastructure.Logger;
using NUnit.Framework;

namespace API.Tests
{
    public class BaseApiTest
    { 
        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
        }

        [SetUp]
        public void BaseSetUp()
        {
        }

        [TearDown]
        public void BaseTearDown()
        {
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            RemoveAllGeneratedFiles();
        }

        protected static void CheckApiErrorMessage(string response, string errorMessage, bool checkPartial = false)
        {
            if (errorMessage != null)
            {
                if (checkPartial)
                {
                    response.Should().Contain(errorMessage);
                }
                else
                {
                    response.Should().Be(errorMessage);
                }
            }
        }

        private static void RemoveAllGeneratedFiles()
        {
            LogHelper.Instance.Info("Removing all generated files");
            var filesFilter = new[] { "*.csv" };
            var files = filesFilter.SelectMany(filter =>
                Directory.GetFiles(Directory.GetCurrentDirectory(), filter));
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    LogHelper.Instance.Warning($"Cannot remove generated file '{file}': {e.Message}");
                }
            }
        }
    }
}