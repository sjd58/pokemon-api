using pokemon_filereader.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

                    SqlCommand cmd = new SqlCommand("SELECT * FROM pokemon ORDER BY pokemon_number;", conn);
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

        public List<Pokemon> GetPokemonByName(string name)
        {
            List<Pokemon> listToReturn = new List<Pokemon>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // TODO use parameterized query instead!
                    // Building the search string like this leaves me vulnerable to sql injection attacks.
                    string sqlSearchString = $"SELECT * FROM pokemon WHERE pokemon.name LIKE '%{name}%' ORDER BY pokemon_number;";
                    SqlCommand cmd = new SqlCommand(sqlSearchString, conn);
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

        public List<Pokemon> GetAndFilterPokemon(string name, int hp, int attack, int defense, int page)
        {
            List<Pokemon> listToReturn = new List<Pokemon>();
            
            // we need to build the sql command based on what we receive from the query.
            // first we're going to build a list of things to search and filter for based on values that are different from the default values
            // We're just going to check what we have in this list as we build the Sql command below.
            // We are NOT going to use string concatenation or interpolation with terms directly query string; this leaves us vulnerable to Sql injection attacks.
            List<string> searchAndFilterTerms = new List<string>();
            if (name != "")
            {
                searchAndFilterTerms.Add("name");
            }
            if (hp > 0)
            {
                searchAndFilterTerms.Add("hp");
            }
            if (attack > 0)
            {
                searchAndFilterTerms.Add("attack");
            }
            if (defense > 0)
            {
                searchAndFilterTerms.Add("defense");
            }

            string SqlCommandString = "SELECT * FROM pokemon ";

            // we're going to use a for loop here to make sure that we add the AND operators where they're needed in the SQL command string
            // if we have search and filter terms, add WHERE to the string and begin adding them
            if (searchAndFilterTerms.Count > 0)
            {
                SqlCommandString += "WHERE ";
                for (int i = 0; i < searchAndFilterTerms.Count; i++)
                {
                    // if we need to search by name, add name search to the string
                    if (searchAndFilterTerms[i] == "name")
                    {
                        SqlCommandString += "name LIKE @name ";
                    }
                    if (searchAndFilterTerms[i] == "hp")
                    {
                        SqlCommandString += "hp <= @hp ";
                    }
                    if (searchAndFilterTerms[i] == "attack")
                    {
                        SqlCommandString += "attack <= @attack ";
                    }
                    if (searchAndFilterTerms[i] == "defense")
                    {
                        SqlCommandString += "defense <= @defense ";
                    }
                    // If we're not at the last item in the list, add "AND "
                    if (i != searchAndFilterTerms.Count - 1)
                    {
                        SqlCommandString += "AND ";
                    }
                }
            }

            // Always order by pokemon number; ask the product owner if they want otherwise.
            SqlCommandString += "ORDER BY pokemon_number ";

            // if page is different from the default, add a pagination to the search.
            if (page > 0)
            {
                page *= 10; // multiply the page by 10 so we always have pages that are groups of 10.
                SqlCommandString += "OFFSET @page ROWS FETCH NEXT 10 ROWS ONLY";
            }

            // Always add a semicolon to the end of the string
            SqlCommandString += ";";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlCommandString, conn);

                    // if these values are different from the default, set the parameters
                    if (name != "")
                    {
                        cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                    }
                    if (hp > 0)
                    {
                        cmd.Parameters.AddWithValue("@hp", hp);
                    }
                    if (defense > 0)
                    {
                        cmd.Parameters.AddWithValue("@defense", defense);
                    }
                    if (attack > 0)
                    {
                        cmd.Parameters.AddWithValue("@attack", attack);
                    }
                    if (page > 0)
                    {
                        cmd.Parameters.AddWithValue("@page", page);
                    }
                    cmd.ExecuteNonQuery();
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

        public List<Pokemon> GetPokemonByPage(int page)
        {
            List<Pokemon> listToReturn = new List<Pokemon>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // For pages, I want to return pokemon in groups of 10.
                    // I'm doing so by pokemon_number, so this will also include mega versions of pokemon that share the same pokemon_number
                    // To do so, I'm going to do some quick logic and math on the page int I receive as an argument
                    // If page is less than 2, set start int to 1; else multiply the page number by 10.
                    // This ternary will help us by taking care of unusual inputs like page 0 or negative page numbers, if we get them.
                    int startInt = page < 2 ? 1 : page * 10;
                    // The endInt is going to be ten more than the start int.
                    int endInt = startInt + 10;
                    // Put these into my SQL select string.
                    // TODO Look at Tom's pagination code, use OFFSET
                    string sqlSearchString = $"SELECT * FROM pokemon WHERE pokemon_number BETWEEN {startInt} AND {endInt} ORDER BY pokemon_number;";
                    SqlCommand cmd = new SqlCommand(sqlSearchString, conn);
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
