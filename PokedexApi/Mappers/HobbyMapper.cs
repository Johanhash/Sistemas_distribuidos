using PokedexApi.Dtos;
using PokedexApi.Models;
using PokedexApi.Infraestructure.Soap.Dtos;

namespace PokedexApi.Mappers;

public static class HobbyMapper
{
    public static HobbyResponse ToDto(this Hobby hobby)
    {
        return new HobbyResponse
        {
            Id = hobby.Id,
            Name = hobby.Name,
            Top = hobby.Top
        };
        
    }

    public static Hobby ToModel(this HobbyResponseDto hobby){
        return new Hobby
        {
            Id = hobby.Id,
            Name = hobby.Name,
            Top = hobby.Top
        };
    }
}