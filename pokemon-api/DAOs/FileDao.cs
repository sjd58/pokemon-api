using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace pokemon_api.DAOs
{
    public class FileDao : IFileDao
    {
        private readonly string connectionString;
        private readonly string CSVFilePath;

        // Constructor to add CSV file path and connection string as properties.
        public FileDao(string connString, string csvPath)
        {
            connectionString = connString;
            CSVFilePath = csvPath;
        }

        // Method to check whether or not the database has any data added to it.
        // Returns a bool; the method below will be called to add data if this returns false.
        public bool HasData()
        {
            string sampleData = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 name FROM pokemon;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        sampleData = Convert.ToString(reader["name"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception happened while checking to see if the database has data: {e}");
            }
            if (string.IsNullOrEmpty(sampleData))
            {
                return false;
            }
            return true;
        }

        public void AddPokemonData()
        {
            try
            {
                // Create a new Sql connection called "conn" using the connection string
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Now we're going to use a Streamreader to get the information we need from the CSV file.
                    try
                    {
                        using (StreamReader reader = new StreamReader(CSVFilePath))
                        {
                            int lineNumber = 0; // declare a line number counter to use so we don't read the first line of the csv file.
                            // while not at the end of the stream, do the following:
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                if (lineNumber != 0)
                                {
                                    string[] values = line.Split(',');

                                    // Create a new Pokemon object, passing in the raw data into the constructor.
                                    Pokemon pokemon = new Pokemon(values);

                                    // If it's not the case that the Pokemon is type Ghost or Legendary, add the Pokemon to the database.
                                    if (!((pokemon.Type1 == "Ghost") || (pokemon.Type2 == "Ghost") || (pokemon.Legendary == true)))
                                    {
                                        SqlCommand cmd = new SqlCommand("INSERT INTO pokemon(pokemon_number,name,type1,type2,total,hp,attack,defense,sp_attack,sp_defense,speed,generation,legendary) values (@pokemon_number, @name, @type1, @type2, @total, @hp, @attack, @defense, @sp_attack, @sp_defense, @speed, @generation, @legendary)", conn);
                                        cmd.Parameters.AddWithValue("@pokemon_number", pokemon.PokemonNumber);
                                        cmd.Parameters.AddWithValue("@name", pokemon.Name);
                                        cmd.Parameters.AddWithValue("@type1", pokemon.Type1);
                                        cmd.Parameters.AddWithValue("@type2", pokemon.Type2);
                                        cmd.Parameters.AddWithValue("@total", pokemon.Total);
                                        cmd.Parameters.AddWithValue("@hp", pokemon.HP);
                                        cmd.Parameters.AddWithValue("@attack", pokemon.Attack);
                                        cmd.Parameters.AddWithValue("@defense", pokemon.Defense);
                                        cmd.Parameters.AddWithValue("@sp_attack", pokemon.SpAttack);
                                        cmd.Parameters.AddWithValue(@"sp_defense", pokemon.SpDefense);
                                        cmd.Parameters.AddWithValue(@"speed", pokemon.Speed);
                                        cmd.Parameters.AddWithValue(@"generation", pokemon.Generation);
                                        cmd.Parameters.AddWithValue(@"legendary", pokemon.Legendary);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                lineNumber++;
                            }
                        }
                    }
                    catch (Exception streamException)
                    {
                        Console.WriteLine($"An exception happened while the StreamReader was working: {streamException}");
                    }
                    conn.Close();
                }
            }
            catch (Exception SqlException)
            {
                Console.WriteLine($"An exception happened while the SqlConnection for the AddPokemonData method was working: {SqlException}");
            }
        }
    }
}
