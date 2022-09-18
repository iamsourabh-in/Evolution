using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PlatformService.Constants;
using Prometheus;

namespace PlatformService.Middleware
{
    public class EndpointRequestCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public EndpointRequestCounterMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {

           var counter = Metrics.CreateCounter(
                    Metrices.PlatformApiRequestCounter.Name,
                    Metrices.PlatformApiRequestCounter.Description,
                    new CounterConfiguration
                    {
                        LabelNames = Metrices.PlatformApiRequestCounter.Labels
                    }
                );

                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();

            await _next(context);
        }
    }
}
