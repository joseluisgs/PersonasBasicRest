using Microsoft.EntityFrameworkCore;
using PersonasBasicRest.Database;
using PersonasBasicRest.Logger;

var logger = LoggerUtils<Program>.GetLogger();

var builder = WebApplication.CreateBuilder(args); // Crea una nueva instancia de WebApplicationBuilder con los argumentos de entrada

// Añadimos los servicios necesarios para la aplicación web

// Añadimos los contextos de la base de datos en memoria para pruebas
builder.Services.AddDbContext<HeroDbContext>(options =>
    {
        options.UseInMemoryDatabase("Heroes")
            .EnableSensitiveDataLogging();
        logger.Debug("In-memory database added");
    }
);
logger.Debug("Heroes in-memory database added");


// Swagger/OpenAPI necesita estos servicios para funcionar
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Agrega el explorador de endpoints API para Swagger/OpenAPI
builder.Services.AddSwaggerGen(); // Agrega SwaggerGen para generar documentación de la API
logger.Debug("Swagger/OpenAPI services added");



// Añade el middleware Swagger/OpenAPI a la aplicación web


var app = builder.Build();  // Crea una nueva instancia de WebApplication con los servicios configurados

// Si estamos en modo desarrollo, habilita Swagger/OpenAPI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Habilita redirección HTTPS si está habilitado

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


// Definice una ruta para obtener pronósticos meteorológicos
app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

logger.Debug("Weather forecast route registered");

app.Run(); // Inicia la aplicación web

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}