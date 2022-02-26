namespace FastApiIntegration.API.Services;

public interface ITemperatureService
{
    public int GenerateTemperature(int min, int max);
}
