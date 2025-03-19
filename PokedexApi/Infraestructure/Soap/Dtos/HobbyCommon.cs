using System.Runtime.Serialization;

namespace PokedexApi.Infraestructure.Soap.Dtos;

[DataContract(Name = "HobbyCommon", Namespace = "http://pokemon-api/hobby-service")]
[KnownType(typeof(CreateHobbyDto))]
[KnownType(typeof(UpdateHobbyDto))]
public class HobbyCommonDto
{
    [DataMember(Name = "Name", Order = 1)]
    public required string Name { get; set; }
    [DataMember(Name = "Top", Order = 2)]
    public int Top { get; set; }
}