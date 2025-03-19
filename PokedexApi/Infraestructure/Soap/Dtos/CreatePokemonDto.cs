namespace PokedexApi.Infraestructure.Soap.Dtos;
using System.Runtime.Serialization;

[DataContract(Name = "CreatePokemonDto", Namespace = "http://pokemon-api/pokemon-service")]

public class CreatePokemonDto : PokemonCommonDto
{
    
}
