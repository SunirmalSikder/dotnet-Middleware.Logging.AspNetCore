using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class ResponseLoggingMiddleware : LoggingMiddleware
    {
        private readonly LoggingMiddlewareConfiguration configuration;

        public ResponseLoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation, LoggingMiddlewareConfiguration configuration)
            : base(loggerFactory, correlation)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ResponseLoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation)
            : this(loggerFactory, correlation,
                new LoggingMiddlewareConfiguration(
                    LogEventLevel.Information,
                    "Outgoing response - {StatusCode}, {Headers}",
                    context => new object[] { context.Response.StatusCode, context.Request.Headers }))
        {
        }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            logger.Log(correlation, configuration.LogEventLevel, configuration.LogMessageTemplate, configuration.LogMessageParameters(context));
        }
    }
}