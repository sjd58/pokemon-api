using pokemon_filereader.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace pokemon_api.DAOs
{
    public class PokemonDao : IPokemonDao
    {
        private readonly string connectionString;

        // set up the constructor so the connection string needs to be passed in when this class is instantiated
        public PokemonDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Pokemon> GetPokemon()
        {
            List<Pokemon> listToReturn = new List<Pokemon>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM pokemon", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pokemon pokemon = GetPokemonFromReader(reader);
                        listToReturn.Add(pokemon);
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"An exception happened while reading from the database: {e}");
            }

            return listToReturn;
        }

        // Helper method only used by this object to create a new Pokemon based off what the SqlDataReader reads.
        private Pokemon GetPokemonFromReader(SqlDataReader reader)
        {
            Pokemon pokemon = new Pokemon()
            {
                PokemonNumber = Convert.ToInt32(reader["pokemon_number"]),
                Name = Convert.ToString(reader["name"]),
                Type1 = Convert.ToString(reader["type1"]),
                Type2 = Convert.ToString(reader["type2"]),
                Total = Convert.ToInt32(reader["total"]),
                HP = Convert.ToInt32(reader["hp"]),
                Attack = Convert.ToInt32(reader["attack"]),
                Defense = Convert.ToInt32(reader["defense"]),
                SpAttack = Convert.ToInt32(reader["sp_attack"]),
                SpDefense = Convert.ToInt32(reader["sp_defense"]),
                Speed = Convert.ToInt32(reader["speed"]),
                Generation = Convert.ToInt32(reader["generation"]),
                Legendary = Convert.ToBoolean(reader["legendary"])
            };

            return pokemon;
        }
    }
}
