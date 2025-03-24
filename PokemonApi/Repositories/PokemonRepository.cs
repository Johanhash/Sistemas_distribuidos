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

    public async Task DeleteAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Remove(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        await _context.Pokemons.AddAsync(pokemon.ToEntity(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        _context.Pokemons.Update(pokemon.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }   

    public async Task<List<Pokemon>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var Pokemons = await _context.Pokemons.AsNoTracking().Where(s=>s.Name == name).ToListAsync(cancellationToken);
        return Pokemons.Select(s=>s.ToModel()).ToList();
    }
}