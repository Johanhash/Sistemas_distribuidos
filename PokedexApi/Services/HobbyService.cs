using PokedexApi.Repositories;
using PokedexApi.Models;

namespace PokedexApi.Services
{
    public class HobbyService : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository;

        public HobbyService(IHobbyRepository hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
        }

        public async Task<Hobby?> GetHobbyByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _hobbyRepository.GetHobbyByIdAsync(id, cancellationToken);
        }

        public async Task<Hobby?> GetHobbyByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _hobbyRepository.GetHobbyByNameAsync(name, cancellationToken);
        }
    }
}