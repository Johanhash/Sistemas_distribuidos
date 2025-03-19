using System.ServiceModel;
using PokedexApi.Infraestructure.Soap.Dtos;

namespace PokedexApi.Infraestructure.Soap.Contracts
{
    [ServiceContract(Name = "HobbyService", Namespace = "http://pokemon-api/hobby-service")]
    public interface IHobbyService
    {
        [OperationContract]
        Task<HobbyResponseDto> GetHobbyByIdAsync(int id, CancellationToken cancellationToken);

        [OperationContract]
        Task<bool> DeleteHobby(int id, CancellationToken cancellationToken);
        
        [OperationContract]
        Task<HobbyResponseDto> CreateHobby(CreateHobbyDto createHobbyDto, CancellationToken cancellationToken);

        [OperationContract]
        Task<HobbyResponseDto> UpdateHobby(UpdateHobbyDto hobby, CancellationToken cancellationToken);

        [OperationContract]
        Task<HobbyResponseDto> GetHobbyByNameAsync(string name, CancellationToken cancellationToken);
    }
}