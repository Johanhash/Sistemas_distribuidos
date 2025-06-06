using PokedexApi.Infraestructure.Grpc;
using PokedexApi.Models;

namespace PokedexApi.Services;

public interface ITrainerService
{
    Task<Trainer?> GetTrainerByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Trainer>> GetAllByNameAsync(string name, CancellationToken cancellationToken);
    Task<(int SuccessCount, List<Trainer> CreatedTrainers)>CreateTrainersAsync(List<Trainer> trainers, CancellationToken cancellationToken);
}
