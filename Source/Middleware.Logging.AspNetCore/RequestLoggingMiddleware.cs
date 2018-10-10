using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class RequestLoggingMiddleware : LoggingMiddleware
    {
        private readonly LoggingMiddlewareConfiguration configuration;

        public RequestLoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation, LoggingMiddlewareConfiguration configuration)
            : base(loggerFactory, correlation)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public RequestLoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation)
            : this(loggerFactory, correlation,
                new LoggingMiddlewareConfiguration(
                    LogEventLevel.Information,
                    "Incoming request - {Method}: {Path}, {Headers}",
                    context => new object[] { context.Request.Method, context.Request.Path.Value, context.Request.Headers }))
        {
        }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.Log(correlation, configuration.LogEventLevel, configuration.LogMessageTemplate, configuration.LogMessageParameters(context));

            await next(context);
        }
    }
}