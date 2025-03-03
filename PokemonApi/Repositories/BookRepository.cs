using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure;
using PokemonApi.Models;
using PokemonApi.Mappers;


namespace PokemonApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RelationalDbContext _context;
        public BookRepository(RelationalDbContext context ){
            _context = context;
        }
        public async Task<List<Book>> GetBookByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await _context.Books
            .Where(b => b.Title.Contains(title))
            .Select(b => b.ToModel())
            .ToListAsync(cancellationToken);
        }
    }

}