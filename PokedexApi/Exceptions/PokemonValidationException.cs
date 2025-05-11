namespace PokedexApi.Exceptions
{
    public class PokemonValidationException : Exception
    {
        public PokemonValidationException(string message) : base(message)
        {

        }
    }
    public class PokemonAlreadyExistsException : Exception
    {
        public string PokemonName { get; }

        public PokemonAlreadyExistsException(string pokemonName)
            : base($"A Pokemon with the name '{pokemonName}' already exists.")
        {
            PokemonName = pokemonName;
        }
    }
}