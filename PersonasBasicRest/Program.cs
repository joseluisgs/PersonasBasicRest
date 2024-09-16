using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonasBasicRest.Database;
using PersonasBasicRest.Logger;


Console.OutputEncoding = Encoding.UTF8; // Necesario para mostrar emojis

var logger = LoggerUtils<Program>.GetLogger();

var builder =
    WebApplication
        .CreateBuilder(args); // Crea una nueva instancia de WebApplicationBuilder con los argumentos de entrada

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

// Añadimos los controladores a la aplicación
builder.Services.AddControllers();
logger.Debug("Controllers added");


// Swagger/OpenAPI necesita estos servicios para funcionar
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Añade el middleware Swagger/OpenAPI a la aplicación web
builder.Services.AddEndpointsApiExplorer(); // Agrega el explorador de endpoints API para Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    // Otros metadatos de la API
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PersonasBasicRest API",
        Description = "An API to perform CRUD operations on heroes",
        TermsOfService = new Uri("https://joseluisgs.dev/docs/license/"),
        Contact = new OpenApiContact
        {
            Name = "José Luis González Sánchez",
            Email = "joseluis.gonzalez@iesluisvives.org",
            Url = new Uri("https://joseluisgs.dev"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under Creative Commons License",
            Url = new Uri("https://joseluisgs.dev/docs/license/"),
        }
    });
}); // Agrega SwaggerGen para generar documentación de la API
logger.Debug("Swagger/OpenAPI services added");


var app = builder.Build(); // Crea una nueva instancia de WebApplication con los servicios configurados

// Si estamos en modo desarrollo, habilita Swagger/OpenAPI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Habilita redirección HTTPS si está habilitado

app.UseRouting(); // Añadir esto si utilizas MVC para definir rutas, decimos que activamos el uso de rutas
// app.UseAuthorization();  // Añadir esto si utilizas autorizaciones

app.MapControllers(); // Añade los controladores a la ruta predeterminada

// Otra forma es utilizar un middleware para interceptar todas las peticiones y manejarlas en un solo lugar
// app.UseEndpoints(endpoints => endpoints.MapControllers());

logger.Debug("Endpoints mapped");


logger.Information("🚀 PersonasBasicRest API started 🟢"); // Registra que la API ha iniciado en el log
Console.WriteLine("🚀 PersonasBasicRest API started 🟢"); // Muestra un mensaje en la consola

app.Run(); // Inicia la aplicación web
