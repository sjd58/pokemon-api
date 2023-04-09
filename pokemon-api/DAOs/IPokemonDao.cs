using System.Collections.Generic;

namespace pokemon_api.DAOs
{
    public interface IPokemonDao
    {
        List<Pokemon> GetAndFilterPokemon(string name, int hp, int attack, int defense, int page);
    }
}
