using PokedexApi.Repositories;
using PokedexApi.Models;

namespace PokedexApi.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        }

        public async Task<Pokemon?> GetPokemonByNameAsync (string name, CancellationToken cancellationToken)
        {
            return await _pokemonRepository.GetPokemonByNameAsync(name, cancellationToken);
        }
    }
}