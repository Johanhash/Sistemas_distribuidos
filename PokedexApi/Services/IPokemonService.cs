
using System.ServiceModel;
using PokedexApi.Models;

namespace PokedexApi.Services
{
    public interface IPokemonService
    {
        [OperationContract]
        Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}