using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonasBasicRest.Models;

namespace PersonasBasicRest.Database;

/**
 * Representa un héro en la base de datos.
 */
[Table("Heroes")]
public class HeroEntity
{
    public const long NewId = 0; // Constante que representa un nuevo ID para un héroe


    [Key] // Atributo que identifica cada héroe como clave primaria
    [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)] // Autoincrementa el valor de Id cada vez que se crea un nuevo héroe
    public long Id { get; set; } = NewId; // Inicializa el Id con NewId cuando se crea un nuevo héroe

    [Required] // Atributo que indica que el nombre es requerido
    [MaxLength(100)] // Atributo que limita la longitud del nombre a 100 caracteres
    public string Name { get; set; } = string.Empty;

    [Required] // Atributo que indica que la descripción es requerida
    [DefaultValue(false)]
    public bool IsBad { get; set; } = false; // Atributo que indica si el héroe es malvado o no

    public DateTime CreatedAt { get; set; } = DateTime.Now; // Agrega la fecha de creación del héroe al crearlo
    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Agrega la fecha de actualización del héroe al modificarlo
    
    public void FromModel(Hero hero)
    {
        Name = hero.Name;
        IsBad = hero.IsBad;
    }
}