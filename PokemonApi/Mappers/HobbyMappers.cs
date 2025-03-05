using PokemonApi.Dtos;
using PokemonApi.infrastructure.Entities;
using PokemonApi.Models;

namespace PokemonApi.Mappers;

public static class HobbyMapper
{
    public static HobbyEntity ToEntity(this Hobby hobby)
    {
        return new HobbyEntity
        {
            Id = hobby.Id,
            Name = hobby.Name,
            Top = hobby.Top
        };
    }

    public static Hobby ToModel(this HobbyEntity entity)
    {
        if (entity is null)
        {
            return null;
        }
        return new Hobby
        {
            Id = entity.Id,
            Name = entity.Name,
            Top = entity.Top
        };
    }

    public static HobbyResponseDto ToDto(this Hobby entity)
    {
        return new HobbyResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Top = entity.Top
        };
    }

    public static Hobby ToModel(this CreateHobbyDto hobby)
    {
        return new Hobby
        {
            Id = new Random().Next(1, int.MaxValue),
            Name = hobby.Name,
            Top = hobby.Top
        };
    }
}