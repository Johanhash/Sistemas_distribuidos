using System.ServiceModel;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Infraestructure.Soap.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PokedexApi.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ILogger<PokemonRepository> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonRepository(ILogger<PokemonRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            var endpoint = new EndpointAddress(configuration.GetValue<string>("PokemonServiceEndpoint"));
            var binding = new BasicHttpBinding();
            _pokemonService = new ChannelFactory<IPokemonService>(binding, endpoint).CreateChannel();
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
                return pokemon.ToModel();
            }
            catch (FaultException ex) when (ex.Message == "Pokemon not found")
            {
                _logger.LogWarning(ex, "Failed to get pokemon with id {id}", id);
                return null;
            }
        }
    }
}

