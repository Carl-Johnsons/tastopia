using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace PostService.API.Middleware;

public class GlobalHandlingErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalHandlingErrorMiddleware> _logger;

    public GlobalHandlingErrorMiddleware(RequestDelegate next, ILogger<GlobalHandlingErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = ex.Message
            };
            string jsonResponse = JsonConvert.SerializeObject(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalHandlingErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalHandlingErrorMiddleware>();
    }
}
