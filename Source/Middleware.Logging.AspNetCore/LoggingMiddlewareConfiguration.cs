using System;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class LoggingMiddlewareConfiguration
    {
        public LogEventLevel LogEventLevel { get; }
        public string LogMessageTemplate { get; }
        public Func<HttpContext, object[]> LogMessageParameters { get; }

        public LoggingMiddlewareConfiguration(LogEventLevel logEventLevel, string logMessageTemplate, Func<HttpContext, object[]> logMessageParameters)
        {
            LogEventLevel = logEventLevel;
            LogMessageTemplate = logMessageTemplate;
            LogMessageParameters = logMessageParameters;
        }
    }
}