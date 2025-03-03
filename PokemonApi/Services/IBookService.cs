using System.ServiceModel;
using PokemonApi.Dtos;
namespace PokemonApi.Services
{
    [ServiceContract (Name = "BookService", Namespace = "http://pokemon-api/book-service" )]
    public interface IBookService
    {  
        [OperationContract]
        Task<List<BookResponseDto>> GetBookByTitle(string title, CancellationToken cancellationToken);
    }

}