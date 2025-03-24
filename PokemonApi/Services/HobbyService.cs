using System.ServiceModel;
using HobbyValidator;
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

    public async Task<HobbyResponseDto> GetHobbyByName(string name, CancellationToken cancellationToken)
    {
        var hobby = await _hobbyRepository.GetByNameAsync(name, cancellationToken);
        if (hobby.Count == 0)
        {
            throw new FaultException("Hobby not found");
        }
        return hobby.First().ToDto();
    }

    public async Task<HobbyResponseDto> CreateHobby(CreateHobbyDto createHobbyDto, CancellationToken cancellationToken)
    {
        var hobbyToCreate = createHobbyDto.ToModel();
        hobbyToCreate.ValidateNames();
        await _hobbyRepository.AddAsync(hobbyToCreate, cancellationToken);
        return hobbyToCreate.ToDto();
    }

    public async Task<HobbyResponseDto> UpdateHobby(UpdateHobbyDto hobby, CancellationToken cancellationToken)
    {
        var hobbyToUpdate = await _hobbyRepository.GetByIdAsync(hobby.Id, cancellationToken);
        if (hobbyToUpdate is null)
        {
            throw new FaultException("Hobby not found");
        }
        hobbyToUpdate.Name = hobby.Name;
        hobbyToUpdate.Top= hobby.Top;
        await _hobbyRepository.UpdateAsync(hobbyToUpdate, cancellationToken);
        return hobbyToUpdate.ToDto();
    }
}