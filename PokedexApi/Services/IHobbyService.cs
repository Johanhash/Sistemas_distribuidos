using System.ServiceModel;
using PokedexApi.Models;

namespace PokedexApi.Services
{
    public interface IHobbyService
    {
        [OperationContract]
        Task<Hobby?> GetHobbyByIdAsync(int id, CancellationToken cancellationToken);
        Task<Hobby?> GetHobbyByNameAsync(string name, CancellationToken cancellationToken);

        Task<bool> DeleteHobbyByIdAsync (int id, CancellationToken cancellationToken);
    }
}