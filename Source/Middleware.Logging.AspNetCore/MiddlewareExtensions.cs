using Microsoft.AspNetCore.Builder;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder
                .UseErrorLogging()
                .UsePerformanceLogging()
                .UseRequestLogging()
                .UseResponseLogging();
        }

        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }

        public static IApplicationBuilder UsePerformanceLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PerformanceLoggingMiddleware>();
        }

        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }

        public static IApplicationBuilder UseResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseLoggingMiddleware>();
        }
    }
}