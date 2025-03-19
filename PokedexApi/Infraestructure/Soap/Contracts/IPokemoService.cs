using System.ServiceModel;

using PokedexApi.Infraestructure.Soap.Dtos;
using PokemdexApi.Infraestructure.Soap.Dtos;


namespace PokedexApi.Infraestructure.Soap.Contracts
{
[ServiceContract (Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service" )]
    public interface IPokemonService
    {
        [OperationContract]
        Task<PokemonResponseDto> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);

        [OperationContract]
        Task<bool> DeletePokemon(Guid id, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto createPokemonDto, CancellationToken cancellationToken);

        [OperationContract]
        Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken);
    }
}