using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Mappers;
using PokemonApi.Repositories;
namespace PokemonApi.Services;

public class HobbyService : IHobbyService
{
    private readonly IHobbyRepository _hobbyRepository;

    public HobbyService(IHobbyRepository hobbyRepository)
    {
        _hobbyRepository = hobbyRepository;
    }

    public async Task<HobbyResponseDto> GetHobbyById(int id, CancellationToken cancellationToken)
    {
        var hobby = await _hobbyRepository.GetByIdAsync(id, cancellationToken);
        if (hobby == null)
        {
            throw new FaultException("Hobby not found");
        }
        return hobby.ToDto();
    }

    public async Task<bool> DeleteHobby(int id, CancellationToken cancellationToken)
    {
        var hobby = await _hobbyRepository.GetByIdAsync(id, cancellationToken);
        if (hobby is null)
        {
            throw new FaultException("Hobby not found");
        }
        await _hobbyRepository.DeleteAsync(hobby, cancellationToken);
        return true;
    }

    public async Task<List<HobbyResponseDto>> GetHobbyByName(string name, CancellationToken cancellationToken)
    {
        var hobbies = await _hobbyRepository.GetByNameAsync(name, cancellationToken);
        return hobbies.ConvertAll(h => h.ToDto()); // Convertimos a DTO
    }
}