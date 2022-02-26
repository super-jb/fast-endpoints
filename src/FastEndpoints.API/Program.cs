global using FastEndpoints;

using FastApiIntegration.API.Services;
using FastEndpoints.Swagger;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();

builder.Services.AddSwaggerDoc(settings =>
    {
        settings.Title = "WeatherForecast API";
        settings.Version = "v1";
    },
    serializerSettings: x => 
    {
        x.ContractResolver = new CamelCasePropertyNamesContractResolver();
    },
    tagIndex: 3,
    shortSchemaNames: true);

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.ShortEndpointNames = true;
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();
