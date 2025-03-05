using System.ServiceModel;
using PokemonApi.Models;

namespace HobbyValidator
{
    public static class HobbyValidator
    {
        public static Hobby ValidateNames(this Hobby hobby) =>
            string.IsNullOrEmpty(hobby.Name) ? throw new FaultException("Hobby name is required") : hobby;
    }
}