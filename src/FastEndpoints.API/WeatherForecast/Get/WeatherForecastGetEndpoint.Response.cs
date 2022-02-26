namespace FastApiIntegration.API.WeatherForecast.Get;

public class WeatherForecastGetEndpointResponse
{
    public IEnumerable<Models.WeatherForecast> WeatherForecasts { get; set; }
}
