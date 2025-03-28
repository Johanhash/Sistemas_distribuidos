using System.Runtime.Serialization;

namespace PokemonApi.Dtos;
 [DataContract(Name = "HobbyResponseDto", Namespace = "http://pokemon-api/hobby-service")]
    public class HobbyResponseDto
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "Name", Order = 2)]
        public string Name { get; set; }
        [DataMember(Name = "Top", Order = 3)]
        public int Top { get; set; }
    }
