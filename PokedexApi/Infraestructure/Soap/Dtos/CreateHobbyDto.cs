namespace PokedexApi.Infraestructure.Soap.Dtos;
using System.Runtime.Serialization;

[DataContract(Name = "CreateHobbyDto", Namespace = "http://pokemon-api/hobby-service")]
public class CreateHobbyDto : HobbyCommonDto
{
    
}