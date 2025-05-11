using PokedexApi.Repositories;
using PokedexApi.Models;
using PokedexApi.Exceptions;

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

        public async Task<bool> DeletePokemonByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _pokemonRepository.DeletePokemonByIdAsync(id, cancellationToken);
        }

        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
        {
            var existingPokemon = await _pokemonRepository.GetPokemonByNameAsync(pokemon.Name, cancellationToken);
            if (existingPokemon != null)
            {
                throw new PokemonAlreadyExistsException(pokemon.Name);
            }
            //TODO: validate if pokemon already exists using the name field
            return await _pokemonRepository.CreatePokemonAsync(pokemon, cancellationToken);
        }

        public async Task UpdatePokemonAsync(Guid id, Pokemon pokemon, CancellationToken cancellationToken)
        {
            var pokemons = await _pokemonRepository.GetPokemonByNameAsync(pokemon.Name, cancellationToken);
             if (pokemons != null && pokemons.Id != id) // Verifica si ya existe otro Pokémon con el mismo nombre
            {
            throw new NameValidationException("The name of the Pokémon already exists.");
            }

            if (pokemon.Level <= 0)
            {
            throw new PokemonValidationException("The level of the Pokémon must be greater than 0.");
            }   

            pokemon.Id = id;
            await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
        
        } 
    }
}
           

    