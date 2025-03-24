using System.Runtime.Serialization;

namespace PokedexApi.Infraestructure.Soap.Dtos;

[DataContract(Name = "UpdateHobbyDto", Namespace ="http://pokemon-api/hobby-service")]

public class UpdateHobbyDto : HobbyCommonDto
{
    [DataMember(Name = "Id", Order = 1)]
    public int Id { get; set; }
}