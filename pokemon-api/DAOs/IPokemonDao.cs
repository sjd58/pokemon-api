using pokemon_filereader.Classes;
using System.Collections.Generic;

namespace pokemon_api.DAOs
{
    public interface IPokemonDao
    {
        // This is using the model from the filereader project
        List<Pokemon> GetPokemon();

        List<Pokemon> GetPokemonByName(string name);
        List<Pokemon> GetPokemonByPage(int page);

        List<Pokemon> GetAndFilterPokemon(string name, int hp, int attack, int defense, int page);
    }
}
