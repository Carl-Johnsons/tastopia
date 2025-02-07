using Newtonsoft.Json;
using System.Net;

namespace DuendeIdentityServer.Middleware;

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
        catch (ResultException rex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorResponseDTO errorDTO = new()
            {
                Code = rex.Errors.First().Code,
                Message = rex.Errors.First().Message ?? "",
                Status = rex.Errors.First().StatusCode ?? (int)HttpStatusCode.InternalServerError
            };

            string jsonResponse = JsonConvert.SerializeObject(errorDTO);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDTO.Status;
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorResponseDTO errorDTO = new()
            {
                Code = "General",
                Message = ex.Message ?? "",
                Status = (int)HttpStatusCode.InternalServerError
            };

            string jsonResponse = JsonConvert.SerializeObject(errorDTO);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDTO.Status;
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
