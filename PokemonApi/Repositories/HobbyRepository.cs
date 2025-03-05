using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;

namespace PokemonApi.Repositories;

public class HobbyRepository : IHobbyRepository
{   
    private readonly RelationalDbContext _context;

    public HobbyRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public async Task<Hobby> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var hobby = await _context.Hobbies.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return hobby?.ToModel();
    }

    public async Task DeleteAsync(Hobby hobby, CancellationToken cancellationToken)
    {
        _context.Hobbies.Remove(hobby.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Hobby>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Hobbies
            .Where(s => s.Name.Contains(name))
            .Select(s => s.ToModel())
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Hobby hobby, CancellationToken cancellationToken)
    {
        await _context.Hobbies.AddAsync(hobby.ToEntity(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Hobby hobby, CancellationToken cancellationToken)
    {
        _context.Hobbies.Update(hobby.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }
}