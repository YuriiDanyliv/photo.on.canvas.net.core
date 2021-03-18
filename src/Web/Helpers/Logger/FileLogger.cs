using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace POC.Web.Helpers
{
    public class FileLogger : ILogger
    {
        private readonly string _path;
        private static object _lock = new object();

        public FileLogger(string path)
        {
            _path = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter != null)
            {
                var folderPath = Path.GetDirectoryName(_path);
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                lock (_lock)
                {
                    File.WriteAllTextAsync(_path,
                    DateTime.Now.ToString() + " " + logLevel + ": " + formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}