using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        private readonly LoggingMiddlewareConfiguration configuration;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger logger, LoggingMiddlewareConfiguration configuration)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger logger)
            : this(next, logger, new ResponseLogConfiguration())
        {
        }

        public async Task Invoke(HttpContext context)
        {
            await next(context);

            var (logMessageTemplate, parameters) = configuration.GetLogMessageFormat(context);
            logger.Log(configuration.LogEventLevel, logMessageTemplate, parameters);
        }

        private class ResponseLogConfiguration : LoggingMiddlewareConfiguration
        {
            public ResponseLogConfiguration()
                : base(LogEventLevel.Information)
            {
            }

            public override (string LogMessageTemplate, object[] Parameters) GetLogMessageFormat(HttpContext context)
            {
                return (LogMessageTemplate: "Outgoing response - {StatusCode}, {Headers}",
                    Parameters: new object[] { context.Response.StatusCode, context.Request.Headers });
            }
        }
    }
}