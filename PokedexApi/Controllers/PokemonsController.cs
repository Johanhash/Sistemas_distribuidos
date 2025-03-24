using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;

namespace PokedexApi.Controllers.PokemonsController;


[ApiController]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase
{   
    private readonly IPokemonService _pokemonService;
    public PokemonsController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    //localhost/api/v1/pokemons/123433-84848
    [HttpGet("{id}")]   
    public async Task<ActionResult<PokemonResponse>> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        if (pokemon is null){
            return NotFound();
        }
        return Ok(pokemon.ToDto());
    }   
    
    //localhost/api/v1/pokemons?name=NOMBRE&variable2=VALOR&variable3=VALOR

    [HttpGet]   
    public async Task<ActionResult<PokemonResponse>> GetPokemonByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon is null){
            return NotFound();
        }
        return Ok(pokemon.ToDto());
    }

    [HttpDelete("{id}")]    
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if(deleted){
            return NoContent(); //204
        }
        return NotFound(); //404
    }
}