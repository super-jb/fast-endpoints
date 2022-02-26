namespace FastApiIntegration.API.Services;

public class TemperatureService : ITemperatureService
{
    public int GenerateTemperature(int min, int max)
    {
        var rng = new Random();
        return rng.Next(min, max);
    }
}
