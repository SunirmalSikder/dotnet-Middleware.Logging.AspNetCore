using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class ResponseLoggingMiddleware : LoggingMiddleware
    {
        private readonly LoggingMiddlewareConfiguration configuration;

        public ResponseLoggingMiddleware(ILoggerFactory loggerFactory, LoggingMiddlewareConfiguration configuration)
            : base(loggerFactory)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ResponseLoggingMiddleware(ILoggerFactory loggerFactory)
            : this(loggerFactory,
                new LoggingMiddlewareConfiguration(
                    LogEventLevel.Information,
                    "Outgoing response - {StatusCode}, {Headers}",
                    context => new object[] { context.Response.StatusCode, context.Request.Headers }))
        {
        }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            logger.Log(configuration.LogEventLevel, configuration.LogMessageTemplate, configuration.LogMessageParameters(context));
        }
    }
}