using System;
using System.Collections.Generic;
using System.Text;

namespace pokemon_filereader.Classes
{
    public interface IPokemonSqlDao
    {
        void AddPokemonToDatabase(Pokemon pokemon);
    }
}
