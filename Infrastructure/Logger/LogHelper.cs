using System.IO;
using System.Xml;
using Infrastructure.Utils;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using NUnit.Framework.Internal;

namespace Infrastructure.Logger
{
    public class LogHelper
    {
        public static readonly LogHelper Instance = new();
        private readonly ILog _log;

        public LogHelper()
        {
            var logConfig = new XmlDocument();
            logConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(typeof(LogHelper).Assembly, typeof(Hierarchy));
            XmlConfigurator.Configure(repo, logConfig["log4net"]);
            _log = LogManager.GetLogger(GetType());
        }

        public void Info(string message) => _log.Info(GetLogMessage("[INFO]", message));

        public void UiAction(string message) => _log.Info(GetLogMessage("- [UI ACTION] -", message));

        public void DB(string message) => _log.Info(GetLogMessage("- [DB] -", message));

        public void Api(string message) => _log.Info(GetLogMessage("- [API] -", message));

        public void Warning(string message) => _log.Info(GetLogMessage("! [WARNING] !", message));

        public void Error(string message) => throw new NUnitException(message);

        private static string GetLogMessage(string infoType, string message)
        {
            var testNumber = TestNameUtil.TestId;
            return $"{(testNumber == string.Empty ? string.Empty : $"[{testNumber}] ")}{infoType} {message}";
        }
    }
}