namespace APIGateway.Middleware;

public class HttpsFallbackMiddleware
{
    private readonly RequestDelegate _next;
    public HttpsFallbackMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Try HTTPS first
            context.Request.Scheme = "https";
            await _next(context);
        }
        catch
        {
            // Fallback to HTTP
            context.Request.Scheme = "http";
            await _next(context);
        }
    }
}
