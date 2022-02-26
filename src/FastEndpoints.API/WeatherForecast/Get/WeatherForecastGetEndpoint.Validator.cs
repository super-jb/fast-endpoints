using FastEndpoints.Validation;

namespace FastApiIntegration.API.WeatherForecast.Get;

public class WeatherForecastGetEndpointValidator : Validator<WeatherForecastGetEndpointRequest>
{
    public WeatherForecastGetEndpointValidator()
    {
        RuleFor(x => x.Days)
            .InclusiveBetween(1,5)
            .WithMessage("Days must be between 1 and 5");
    }
}
