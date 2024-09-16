using PersonasBasicRest.Database;
using PersonasBasicRest.Models;

namespace PersonasBasicRest.Mapper;

/**
 * Mapeador de objetos entre la capa de modelo y la capa de datos.
 */
public static class HeroMapper
{
    public static HeroEntity ToEntity(this Hero hero)
    {
        return new HeroEntity
        {
            Id = hero.Id,
            Name = hero.Name,
            IsBad = hero.IsBad
        };
    }

    public static Hero ToModel(this HeroEntity heroEntity)
    {
        return new Hero
        {
            Id = heroEntity.Id,
            Name = heroEntity.Name,
            IsBad = heroEntity.IsBad,
            CreatedAt = heroEntity.CreatedAt,
            UpdatedAt = heroEntity.UpdatedAt
        };
    }
}