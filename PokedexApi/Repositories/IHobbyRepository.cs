using PokedexApi.Models;

namespace PokedexApi.Repositories
{
    public interface IHobbyRepository
    {
        Task<Hobby?> GetHobbyByIdAsync(int id, CancellationToken cancellationToken);
        Task<Hobby?> GetHobbyByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeleteHobbyByIdAsync(int id, CancellationToken cancellationToken);
    }
}