using System.Runtime.Serialization;
using PokedexApi.Infraestructure.Soap.Dtos;

namespace PokedexApi.Infraestructure.Soap.Dtos

{
    [DataContract(Name = "HobbyResponseDto", Namespace = "http://pokemon-api/hobby-service")]
    public class HobbyResponseDto
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public required string Name { get; set; }
        [DataMember(Name = "Top", Order = 3)]
        public int Top { get; set; }
    }
}