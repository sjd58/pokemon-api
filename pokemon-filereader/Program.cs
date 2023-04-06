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

                                    SqlCommand cmd = new SqlCommand("INSERT INTO pokemon(pokemon_number,name,type1,type2,total,hp,attack,defense,sp_attack,sp_defense,speed,generation,legendary) values (@pokemon_number, @name, @type1, @type2, @total, @hp, @attack, @defense, @sp_attack, @sp_defense, @speed, @generation, @legendary)", conn);
                                    cmd.Parameters.AddWithValue("@pokemon_number", int.Parse(values[0]));
                                    cmd.Parameters.AddWithValue("@name", values[1]);
                                    cmd.Parameters.AddWithValue("@type1", values[2]);
                                    cmd.Parameters.AddWithValue("@type2", values[3]);
                                    cmd.Parameters.AddWithValue("@total", int.Parse(values[4]));
                                    cmd.Parameters.AddWithValue("@hp", int.Parse(values[5]));
                                    cmd.Parameters.AddWithValue("@attack", int.Parse(values[6]));
                                    cmd.Parameters.AddWithValue("@defense", int.Parse(values[7]));
                                    cmd.Parameters.AddWithValue("@sp_attack", int.Parse(values[8]));
                                    cmd.Parameters.AddWithValue(@"sp_defense", int.Parse(values[9]));
                                    cmd.Parameters.AddWithValue(@"speed", int.Parse(values[10]));
                                    cmd.Parameters.AddWithValue(@"generation", int.Parse(values[11]));
                                    cmd.Parameters.AddWithValue(@"legendary", bool.Parse(values[12]));

                                    cmd.ExecuteNonQuery();
                                }
                                lineNumber++;

                            }
                        }
                    }
                    catch (Exception f)
                    {
                        Console.WriteLine($"An exception happened while the StreamReader was working: {f}");
                    }
                    conn.Close(); // Close the command
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception happened while the SqlConnection was working: {e}");
            }
        }
    }
}
