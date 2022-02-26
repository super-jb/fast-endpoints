using FastApiIntegration.API.Services;
using FastApiIntegration.API.WeatherForecast.Get;
using FastEndpoints.Validation;

namespace FastApiIntegration.API.Processor;

public class MyPostProcessor<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public Task PostProcessAsync(TRequest req, TResponse res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
    {
        var logger = ctx.RequestServices.GetRequiredService<ILogger<TResponse>>();
        var auditService = ctx.RequestServices.GetRequiredService<IAuditService>();

        if (res is WeatherForecastGetEndpointResponse response)
        {
            int count = response.WeatherForecasts.Count();
            logger.LogWarning($"{count} retrieved");
            auditService.RecordAudit("GET", $"{count} retrieved");
        }
        logger.LogInformation("something happened");

        return Task.CompletedTask;
    }
}
