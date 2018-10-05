using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public abstract class LoggingMiddlewareConfiguration
    {
        public LogEventLevel LogEventLevel { get; }

        protected LoggingMiddlewareConfiguration(LogEventLevel logEventLevel)
        {
            LogEventLevel = logEventLevel;
        }

        public abstract (string LogMessageTemplate, object[] Parameters) GetLogMessageFormat(HttpContext context);
    }
}