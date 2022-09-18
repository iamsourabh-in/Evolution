using System;
using Microsoft.AspNetCore.Builder;
using PlatformService.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseEndpointRequestCounterMiddleware(
        this IApplicationBuilder app)
        => app.UseMiddleware<EndpointRequestCounterMiddleware>();

}