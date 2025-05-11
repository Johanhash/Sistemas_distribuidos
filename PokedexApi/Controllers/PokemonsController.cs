using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Infraestructure.Soap.Dtos;
using System.ServiceModel.Channels;
using PokedexApi.Models;
using PokedexApi.Exceptions;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(Policy = "Read")]
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
    [Authorize(Policy = "Write")]  
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if(deleted){
            return NoContent(); //204
        }
        return NotFound(); //404
    }

    //400 Bad request (Usuario ingresado un valor incorrecto)
    //409 - Conflic (Ya existe el recurso que se requiere crear)
    //200 - Ok (Objeto de respuesta, Se creo el recurso)
    //201 - Created (Pokemon creado, en headers de respuesta url de recurso creado)
    [HttpPost]
    public async Task<ActionResult<PokemonResponse>> CreatePokemon([FromBody] CreatePokemonRequest pokemon, CancellationToken cancellationToken)
    {
        try{
        var createdPokemon = await _pokemonService.CreatePokemonAsync(pokemon.ToModel(), cancellationToken);
        return CreatedAtAction(nameof(GetPokemonById), new {id = createdPokemon.Id}, createdPokemon.ToDto());
        }
        catch(PokemonValidationException ex){
            return BadRequest(new{message = ex.Message});
        }
        catch (PokemonAlreadyExistsException ex)
        {
            return Conflict(new { message = $"Pokemon '{ex.PokemonName}' already exists", exception = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePokemon(Guid id, [FromBody] UpdatePokemonRequest pokemon, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonService.UpdatePokemonAsync(id, pokemon.ToModel(), cancellationToken);
            return NoContent();
        }
        catch (NameValidationException)
        {
            return Conflict(new {message=$"Pokemon alredy exists with the name:",pokemon.Name});
        }
        catch (PokemonValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (PokemonNotFoundException)
        {
            return NotFound();
        }
    }
}