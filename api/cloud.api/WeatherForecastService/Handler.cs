using MediatR;
using cloud.api.WeatherForecastService.Models;

namespace cloud.api.WeatherForecastService;

public class GetWeatherForecastRequest : IRequest<WeatherForecast[]>
{
    public int Days { get; set; }
}

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, WeatherForecast[]>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<WeatherForecast[]> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        return Enumerable.Range(1, request.Days).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

public static class EndPointRegistration
{
    public static void RegisterGetWeatherForecastEndPoint(this WebApplication app)
    {
        app.MapGet("/weatherforecast", async (IMediator mediator) =>
        {
            return await mediator.Send(new GetWeatherForecastRequest { Days = 25 });
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
