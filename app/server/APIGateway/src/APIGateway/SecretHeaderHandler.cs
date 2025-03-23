namespace APIGateway;

internal class SecretHeaderHandler : DelegatingHandler
{
    public SecretHeaderHandler()
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                           CancellationToken cancellationToken)
    {
        var gatewayToken = DotNetEnv.Env.GetString("API_GATEWAY_SECRET");
        request.Headers.Add("X-ApiGateway-Header", gatewayToken);
        return base.SendAsync(request, cancellationToken);
    }
}
