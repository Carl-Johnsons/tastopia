using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Contract.Middleware;

public class ValidateGatewayRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidateGatewayRequestMiddleware> _logger;

    public ValidateGatewayRequestMiddleware(RequestDelegate next,
                                            ILogger<ValidateGatewayRequestMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
           await _next(context);
           return;
        }

        var expectedToken = DotNetEnv.Env.GetString("API_GATEWAY_SECRET");

        if (!context.Request.Headers.TryGetValue("X-ApiGateway-Header", out var headerValue))
        {
           _logger.LogError("Gateway way token missing");
           context.Response.StatusCode = StatusCodes.Status401Unauthorized;
           await context.Response.WriteAsync("Unauthorized");
           return;
        }

        if (!string.Equals(headerValue, expectedToken, StringComparison.Ordinal))
        {
           _logger.LogError($"Invalid X-ApiGateway-Header value: {headerValue}");
           context.Response.StatusCode = StatusCodes.Status401Unauthorized;
           await context.Response.WriteAsync("Unauthorized: invalid gateway token");
           return;
        }

        await _next(context);
    }
}


