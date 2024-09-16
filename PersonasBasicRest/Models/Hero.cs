namespace PersonasBasicRest.Models;

/**
 * Representa un héroe en la aventura.
 */
public class Hero
{
    public long Id { get; set; } = 0; // Atributo que identifica
    public string Name { get; set; } = string.Empty;
    public bool IsBad { get; set; } = false; // Atributo que indica si el héroe es malvado o no
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Agrega la fecha de creación del héroe al crearlo
    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Agrega la fecha de actualización del héroe al modificarlo
}