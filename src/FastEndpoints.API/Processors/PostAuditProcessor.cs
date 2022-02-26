using FastApiIntegration.API.Services;
using FastApiIntegration.API.WeatherForecast.Get;
using FastEndpoints.Validation;

namespace FastApiIntegration.API.Processors;

public class PostAuditProcessor<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public Task PostProcessAsync(TRequest req, TResponse res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
    {
        var logger = ctx.RequestServices.GetRequiredService<ILogger<TResponse>>();
        var auditService = ctx.RequestServices.GetRequiredService<IAuditService>();

        if (res is WeatherForecastGetEndpointResponse response)
        {
            string message = $"{response.WeatherForecasts.Count()} forecasts retrieved";
            logger.LogWarning(message);
            auditService.RecordAudit("GET", message);
        }
        logger.LogInformation("something happened");

        return Task.CompletedTask;
    }
}
