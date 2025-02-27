using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Mappers;
using PokemonApi.Repositories;
namespace PokemonApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookResponseDto>> GetBookByTitle(string title, CancellationToken cancellationToken)
    {
        
        var books = await _bookRepository.GetBookByTitleAsync(title, cancellationToken);
        return books.ConvertAll(h => h.ToDto()); // Convertimos a DTO
    }
}