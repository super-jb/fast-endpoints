using FastApiIntegration.API.Processor;
using FastApiIntegration.API.Services;
using System.Net;

namespace FastApiIntegration.API.WeatherForecast.Get;

public class WeatherForecastGetEndpoint : Endpoint<WeatherForecastGetEndpointRequest, WeatherForecastGetEndpointResponse>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public ITemperatureService TemperatureService { get; init; }

    public override void Configure()
    {
        Get("api/v1/weatherforecast/{days}");
        AllowAnonymous();

        Describe(d => d
          .Accepts<WeatherForecastGetEndpointRequest>("application/json")
          .Produces<WeatherForecastGetEndpointResponse>(200, "application/json")
          .ProducesProblem((int)HttpStatusCode.Forbidden));

        Summary(s => {
            s.Summary = "short summary goes here";
            s.Description = "long description goes here";
            s[200] = "success response description goes here";
            s[403] = "forbidden response description goes here";
        });

        PostProcessors(new MyPostProcessor<WeatherForecastGetEndpointRequest, WeatherForecastGetEndpointResponse>());
    }

    public override async Task HandleAsync(WeatherForecastGetEndpointRequest req, CancellationToken ct)
    {
        var rng = new Random();

        Response.WeatherForecasts =
            Enumerable
                .Range(1, req.Days)
                .Select(index => new Models.WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = TemperatureService.GenerateTemperature(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });

        await SendAsync(Response, cancellation: ct);
    }
}
