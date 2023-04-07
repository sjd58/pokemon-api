using pokemon_filereader.Classes;
using System.Collections.Generic;

namespace pokemon_api.DAOs
{
    public interface IPokemonDao
    {
        // This is using the model from the filereader project
        List<Pokemon> GetPokemon();
    }
}
