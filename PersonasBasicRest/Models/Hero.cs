namespace PersonasBasicRest.Models;

/**
 * Representa un héroe en la aventura.
 */


public class Hero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBadGuy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Agrega la fecha de creación del héroe al crearlo
    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Agrega la fecha de actualización del héroe al modificarlo
    
}