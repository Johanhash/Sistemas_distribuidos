using PokemonApi.Models;
namespace PokemonApi.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetBookByTitleAsync(string Title, CancellationToken cancellationToken);
}