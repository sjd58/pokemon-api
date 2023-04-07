using pokemon_filereader.Classes;
using System;
using System.Data.SqlClient;
using System.IO;

namespace pokemon_filereader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=pokemon_db;Trusted_Connection=True;";
            string csvFilePath = "C:\\Users\\Student\\workspace\\samuel-dickson-student-code\\side-projects\\pokemon-api\\pokemon.csv";
            //UI ui = new UI();
            //ui.Run();


            // We're using something external to this program, so wrap the SQL in a try/catch block 
            try
            {
                // Create a new Sql connection called "conn" using the connection string
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Now we're going to use a Streamreader to get the information we need from the CSV file
                    // Again we're using something outside of this program, therefore it's a good idea to use a try/catch block
                    try
                    {
                        using (StreamReader reader = new StreamReader(csvFilePath))
                        {
                            int lineNumber = 0; // declare a line number counter to use so we don't read the first line of the csv file.
                            // while not at the end of the stream, do the following:
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                if (lineNumber != 0)
                                {
                                    string[] values = line.Split(',');

                                    //string sqlCommand = "INSERT INTO pokemon_db.dbo.pokemon VALUES()";

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
                    conn.Close(); // Close the command
                }
            }
            catch (Exception SqlException)
            {
                Console.WriteLine($"An exception happened while the SqlConnection was working: {SqlException}");
            }
        }
    }
}
