using System.ServiceModel;
using PokemonApi.Dtos;
namespace PokemonApi.Services
{
    [ServiceContract (Name = "HobbyService", Namespace = "http://pokemon-api/hobby-service" )]
    public interface IHobbyService
    {
        [OperationContract]
        Task<HobbyResponseDto> GetHobbyById(int id, CancellationToken cancellationToken);

        [OperationContract]
        Task<bool> DeleteHobby(int id, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<List<HobbyResponseDto>> GetHobbyByName(string name, CancellationToken cancellationToken);
    }
}