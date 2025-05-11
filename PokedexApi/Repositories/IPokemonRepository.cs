using PokedexApi.Models;

namespace PokedexApi.Repositories;

    public interface IPokemonRepository
    {
        Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Pokemon?> GetPokemonByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeletePokemonByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
        Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken);
    }
