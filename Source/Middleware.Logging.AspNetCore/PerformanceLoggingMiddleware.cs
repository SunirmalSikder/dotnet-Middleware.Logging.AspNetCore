using System.Diagnostics;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public class PerformanceLoggingMiddleware : LoggingMiddleware
    {
        public PerformanceLoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation)
            : base(loggerFactory, correlation)
        {
        }

        public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await next(context);

            stopWatch.Stop();
            logger.LogInformation(correlation, "Request executed in {RequestTime:000} ms - {Method}: {Path}",
                stopWatch.ElapsedMilliseconds,
                context.Request.Method,
                context.Request.Path);
        }
    }
}