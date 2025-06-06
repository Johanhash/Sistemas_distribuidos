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
        Task<HobbyResponseDto> GetHobbyByName(string name, CancellationToken cancellationToken);

        [OperationContract]
        Task<HobbyResponseDto> CreateHobby(CreateHobbyDto createHobbyDto, CancellationToken cancellationToken);

        [OperationContract]
        Task<HobbyResponseDto> UpdateHobby(UpdateHobbyDto hobby, CancellationToken cancellationToken);
    }
}