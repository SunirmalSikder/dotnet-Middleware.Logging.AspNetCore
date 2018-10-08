using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public abstract class LoggingMiddleware : IMiddleware
    {
        protected readonly ILogger logger;

        protected LoggingMiddleware(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            logger = loggerFactory.CreateLogger(this);
        }

        public abstract Task InvokeAsync(HttpContext context, RequestDelegate next);
    }
}