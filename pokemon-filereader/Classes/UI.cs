using System;
using System.Collections.Generic;
using System.Text;

namespace pokemon_filereader.Classes
{
    public class UI
    {
        public string InputFile;
        public string SQLConnection;

        public void Run()
        {
            Console.WriteLine("Welcome. The CLI will walk you through the steps to set up the Pokemon database from the CSV file.");
            Console.WriteLine("Then, this application will add Pokemon to the database, only adding the information that it should.");
            Console.WriteLine("Before we begin, make sure that the database and table have been created using the sql files in this project.");
            Console.WriteLine("Let's get started! Please enter the fully qualifeid filepath for the CSV file you're using: ");
            InputFile = Console.ReadLine();
            Console.WriteLine("Okay great. Now please enter the SQL connection string.");
            SQLConnection = Console.ReadLine();
            Console.WriteLine("Thank you. That's the information I needed.");
            Console.WriteLine("Now I'm going to try to add the Pokemon to the database. If the information you added is correct this should work...");


        }
    }
}
