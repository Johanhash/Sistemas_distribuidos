namespace PokedexApi.Exceptions;
public class NameValidationException :Exception
{
    public NameValidationException(String message) : base(message){
        
    }
}