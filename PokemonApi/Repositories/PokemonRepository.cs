using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;

namespace PokemonApi.Repositories;

public class PokemonRepository : IPokemonRepository
{   
    private readonly RelationalDbContext _context;
    public PokemonRepository(RelationalDbContext context ){
        _context = context;
    }

    public async Task<Pokemon> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var Pokemon = await _context.Pokemons.AsNoTracking().FirstOrDefaultAsync(s=>s.Id == id, cancellationToken); //urm
        return Pokemon.ToModel();
    }
}