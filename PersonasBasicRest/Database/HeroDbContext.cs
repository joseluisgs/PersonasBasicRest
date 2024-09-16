using Microsoft.EntityFrameworkCore;
using PersonasBasicRest.Models;

namespace PersonasBasicRest.Database;

/**
 * Contexto de la base de datos para los héroes.
 */
public class HeroDbContext(DbContextOptions<HeroDbContext> options)
    : DbContext(options) // Hereda de DbContext para establecer la conexión con la base de datos. 
{
    public DbSet<HeroEntity> Heroes { get; set; } // Tabla de héroes en la base de datos del modelo
}