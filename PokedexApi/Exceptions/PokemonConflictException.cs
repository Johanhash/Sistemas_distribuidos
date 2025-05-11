namespace PokedexApi.Exceptions;

    public class PokemonConflictException : Exception{
        public PokemonConflictException(string message) : base(message){
            
        }
    }
