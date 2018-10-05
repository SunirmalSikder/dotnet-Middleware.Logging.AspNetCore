using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class PerformanceLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public PerformanceLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await next(context);

            stopWatch.Stop();
            logger.LogInformation("Request executed in {RequestTime:000} ms - {Method}: {Path}",
                stopWatch.ElapsedMilliseconds,
                context.Request.Method,
                context.Request.Path);
        }
    }
}