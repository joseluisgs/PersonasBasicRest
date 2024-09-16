using Microsoft.EntityFrameworkCore;
using PersonasBasicRest.Models;

namespace PersonasBasicRest.Database;

/**
 * Contexto de la base de datos para los héroes.
 */
public class HeroDbContext(DbContextOptions<HeroDbContext> options)
    : DbContext(options) // Hereda de DbContext para establecer la conexión con la base de datos. 
{
    public DbSet<HeroEntity> Heroes { get; set; } // Tabla de héroes en la base de datos del modelo HeroEntity y la base de datos, nos da las consultas
    
    // En el método OnModelCreating se definen las relaciones entre las entidades y las tablas de la base de datos.
    // Tambien se puede hacer por medio de DataAnnotations en las entidades.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Se añade el DbSet para la entidad en la base de datos
        // Lo hace todo con las anotaciones de la entidad, así que solo voy a añadir las fechas
        // Que son las que no se pueden hacer con anotaciones
        modelBuilder.Entity<HeroEntity>(entity =>
        {
            entity.Property(e => e.CreatedAt).IsRequired()
                .ValueGeneratedOnAdd(); // Se define que el campo CreatedAt es obligatorio y se genera automáticamente al añadir un registro
            entity.Property(e => e.UpdatedAt).IsRequired()
                .ValueGeneratedOnUpdate(); // Se define que el campo UpdatedAt es obligatorio y se genera automáticamente al actualizar un registro
        });
    }
}