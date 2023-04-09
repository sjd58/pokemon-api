using System.Collections.Generic;

namespace pokemon_api.DAOs
{
    public interface IFileDao
    {
        bool HasData();
        void AddPokemonData();
    }
}
