using Microsoft.EntityFrameworkCore;
using PersonasBasicRest.Database;
using PersonasBasicRest.Logger;

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


logger.Information("🚀 PersonasBasicRest API started 🟢"); // Registra que la API ha iniciado en el log

app.Run(); // Inicia la aplicación web