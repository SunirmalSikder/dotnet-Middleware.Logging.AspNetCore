using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception from request - {Method}: {Path}",
                    context.Request.Method,
                    context.Request.Path.Value);
            }
        }
    }
}