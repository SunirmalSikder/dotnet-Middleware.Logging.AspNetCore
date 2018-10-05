using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        private readonly LoggingMiddlewareConfiguration configurationForRequestLog;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger logger, LoggingMiddlewareConfiguration configurationForRequestLog)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.configurationForRequestLog = configurationForRequestLog ?? throw new ArgumentNullException(nameof(configurationForRequestLog));
        }

        public RequestLoggingMiddleware(RequestDelegate next, ILogger logger)
            : this(next, logger, new RequestLogConfiguration())
        {
        }

        public async Task Invoke(HttpContext context)
        {
            var (logMessageTemplate, parameters) = configurationForRequestLog.GetLogMessageFormat(context);
            logger.Log(configurationForRequestLog.LogEventLevel, logMessageTemplate, parameters);

            await next(context);
        }

        private class RequestLogConfiguration : LoggingMiddlewareConfiguration
        {
            public RequestLogConfiguration()
                : base(LogEventLevel.Information)
            {
            }

            public override (string LogMessageTemplate, object[] Parameters) GetLogMessageFormat(HttpContext context)
            {
                return (LogMessageTemplate: "Incoming request - {Method}: {Path}, {Headers}",
                    Parameters: new object[] { context.Request.Method, context.Request.Path.Value, context.Request.Headers });
            }
        }
    }
}