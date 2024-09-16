using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonasBasicRest.Database;

/**
 * Representa un héro en la base de datos.
 */
[Table("Heroes")]
public class HeroEntity
{
    [Key] // Atributo que identifica cada héroe como clave primaria
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrementa el valor de Id cada vez que se crea un nuevo héroe
    public int Id { get; set; }
    
    [Required] // Atributo que indica que el nombre es requerido
    [MaxLength(100)] // Atributo que limita la longitud del nombre a 100 caracteres
    public string Name { get; set; } = string.Empty;

    [Required] // Atributo que indica que la descripción es requerida
    [DefaultValue(false)]
    public bool IsBadGuy { get; set; } = false; // Atributo que indica si el héroe es malvado o no

    public DateTime CreatedAt { get; set; } = DateTime.Now; // Agrega la fecha de creación del héroe al crearlo
    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Agrega la fecha de actualización del héroe al modificarlo
}