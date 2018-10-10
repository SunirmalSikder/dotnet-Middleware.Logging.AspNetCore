using System;
using System.Threading.Tasks;
using Affecto.Logging;
using Microsoft.AspNetCore.Http;

namespace Affecto.Middleware.Logging.AspNetCore
{
    public abstract class LoggingMiddleware : IMiddleware
    {
        protected readonly ICorrelationLogger logger;
        protected readonly ICorrelation correlation;

        protected LoggingMiddleware(ILoggerFactory loggerFactory, ICorrelation correlation)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            logger = loggerFactory.CreateCorrelationLogger(this);
            this.correlation = correlation ?? throw new ArgumentNullException(nameof(correlation));
        }

        public abstract Task InvokeAsync(HttpContext context, RequestDelegate next);
    }
}