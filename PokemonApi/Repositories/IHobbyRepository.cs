using PokemonApi.Models;
namespace PokemonApi.Repositories;

public interface IHobbyRepository
{
    Task<Hobby> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task DeleteAsync(Hobby hobby, CancellationToken cancellationToken);
    Task<List<Hobby>> GetByNameAsync(string name, CancellationToken cancellationToken);
}