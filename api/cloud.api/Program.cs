using cloud.api;
using cloud.api.WeatherForecastService;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(c => {
    c.RegisterServicesFromAssemblies(typeof(GetWeatherForecastHandler).Assembly);
    //c.AddRequestPreProcessor(typeof(GetWeatherForecastRequestPreProcessor));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.RegisterGetWeatherForecastEndPoint();

app.Run();

