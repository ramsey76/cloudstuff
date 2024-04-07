using MediatR;
using api.Models;
using MediatR.Pipeline;

namespace api.WeatherForecastServices;

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

public class GetWeatherForecastRequestPreProcessor : IRequestPreProcessor<GetWeatherForecastRequest>
{
    private readonly ILogger<GetWeatherForecastRequestPreProcessor> _logger;

    public GetWeatherForecastRequestPreProcessor(ILogger<GetWeatherForecastRequestPreProcessor> logger)
    {
        _logger = logger;
    }

    public async Task Process(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing request for {Days} days", request.Days);
    }
}

public class GenericPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<GenericPreProcessor<TRequest>> _logger;

    public GenericPreProcessor(ILogger<GenericPreProcessor<TRequest>> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing request for {Request} HELLO WORLD", request);
    }
}
