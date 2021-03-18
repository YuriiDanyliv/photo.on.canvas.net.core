using Microsoft.Extensions.Logging;

namespace POC.Web.Helpers
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}