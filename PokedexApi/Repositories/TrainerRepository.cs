using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Grpc.Core;
using PokedexApi.Infraestructure.Grpc;
using PokedexApi.Mappers;
using PokedexApi.Models;

namespace PokedexApi.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly TrainerService.TrainerServiceClient _client;

    public TrainerRepository(TrainerService.TrainerServiceClient client)
    {
        _client = client;
    }

    public async Task<Trainer?> GetTrainerByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = await _client.GetTrainerAsync(new TrainerByIdRequest { Id = id }, cancellationToken: cancellationToken);

            return trainer.ToModel();
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return null;
        }
    }

    public async IAsyncEnumerable<Trainer> GetAllByNameAsync(string name, [EnumeratorCancellation] CancellationToken cancellationToken)
    {

        var request = new GetTrainersByNameRequest { Name = name };

        using var call = _client.GetTrainersByName(request, cancellationToken: cancellationToken);
        while (await call.ResponseStream.MoveNext(cancellationToken))
        {
            yield return call.ResponseStream.Current.ToModel();
        }
    }
    public async Task<(int SuccessCount, List<Trainer> CreatedTrainers)> CreateTrainersAsync(List<Trainer> trainers, CancellationToken cancellationToken)
    {
        using var call = _client.CreateTrainer(cancellationToken: cancellationToken);

        foreach (var trainer in trainers)
        {
            var request = new CreateTrainerRequest
            {
                Name = trainer.Name,
                Age = trainer.Age,
                Birthdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(trainer.BirthDate),
                Medals = { trainer.Medals.Select(m => new Medals {
                    Region = m.Region,
                    Type = Enum.Parse<MedalType>(m.Type)
                })}
            };
            await call.RequestStream.WriteAsync(request, cancellationToken);
        }
        await call.RequestStream.CompleteAsync();
        var response = await call;

        return (response.SuccessCount, response.Trainers.ToModel());

    }
}