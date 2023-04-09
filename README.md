### Objective

Your assignment is to create a Pokémon API from a CSV file using C# and .Net.

### Brief

Professor Oak is in trouble! A wild Blastoise wreaked havoc in the server room and destroyed every single machine. There are no backups - everything is lost! Professor Oak quickly scribbles down all the Pokémon from memory and hands them to you on a piece of paper. (`/Data/pokemon.csv`). Your task is to restore the Pokémon Database from that file and create a Pokémon API so that they’re never lost again.

### Tasks

-   Implement assignment using:
    -   Platform: **ASP.NET WebForms (Blazor or MVC can be subsituted)**
    -   Language: **C#**
    -   Framework: **.Net**
-   Create a Pokémon Model that includes all fields outlined in `/Data/pokemon.csv`
-   Parse the .csv file and create entries for each row based on the following conditions:
    -   Exclude Legendary Pokémon
    -   Exclude Pokémon of Type: Ghost
    -   For Pokémon of Type: Steel, double their HP
    -   For Pokémon of Type: Fire, lower their Attack by 10%
    -   For Pokémon of Type: Bug & Flying, increase their Attack Speed by 10%
    -   For Pokémon that start with the letter **G**, add +5 Defense for every letter in their name (excluding **G**)
-   Create one API endpoint `/pokemon`
    -   This API endpoint should be searchable, filterable and paginatable
        -   Search: name
            -   Bonus: implement fuzzy search using Levenshtein distance
        -   Filter: HP, Attack & Defense
            -   e.g. `/pokemon?hp[gte]=100&defense[lte]=200`
        -   Pagination: e.g. `/pokemon?page=1`

### Evaluation Criteria

-   **C#** best practices
-   Show us your work through your comments
-   We're looking for you to produce working code, with enough room to demonstrate how to structure components in a small program
-   Completeness: did you complete the features?
-   Correctness: does the functionality act in sensible, thought-out ways?
-   Maintainability: is it written in a clean, maintainable way?
-   Testing: is the system adequately tested? How was testing performed?

### CodeSubmit

Please organize, design, test and document your code as if it were going into production - then zip and send back to CobbleStone Software.

All the best and happy coding,

The CobbleStone Software Team

### Development Log

**4/5/2023:**
- Project folder created. Solution created. Using .NET 3.1.
- Created pokemon_db database, pokemon table, and saved files to create the database and table within the sql folder.
- Implemented preliminary logic to add pokemon rows to the database from the csv file.
- Created GitHub repository, set to private, and made an inital commit. 
TO DO:
- Break apart streamreader and DAO logic into separate classes. 4/9/2023: Upon review, decided the code was managable enough to keep in one method.
- Implement CLI to prompt users for filepaths and the SQL connection string 4/8/2023: Upon review, decided to remove unneeded CLI stand alone application alongside the API which allows users to manually add data. Rather, the API itself now checks for data and adds it to the database if there isn't any. Now users set up the appsettings.json file and the app just works. Much better solution.
- Implement logic to add the information we want to the database, based on the description above. COMPLETED 4/6/2023

**4/6/2023**
- Implemented classes and folders for the API
- Added overloaded constructors to the Pokemon class so that it will modify the Pokemon object based on the project specifications or not, depending on what we pass into the constructor.
- Successfully added only the information we want to the database using the Pokemon constructor.
- As per the directions for this assignment, I'm making sure I'm adding comments to my code throughout to explain my work and thought process.
- Pushed code to Git.
- MY GOALS FOR TODAY: Add Pokemon to the database using the specifications above, have the endpoint returning at least a list of Pokemon. COMPLETED.
TO DO:
- Merge entire project into one solution, right now there's two; one for the service that adds information to the database, one that runs the API. COMPLETED 4/8/2023

**4/7/2023**
- Single endpoint working. Added functional searching, filtering based on HP, Attack, Defense stats, and pagination.
- Pushed code to Git.

**4/8/2023**
- Created BuildDatabase class, FileDao class, and IFileDao interface. Implemented logic to check if the database has data, and if not, to add data without the need for the filereader CLI program alongside the API solution.
- Removed the stand alone pokemon-filereader program from the solution; there is now only one Main method in the solution as there should be.
- Now using ConfigurationBuilder and appsettings.json to store and retrieve my database connection string and my pokemon.csv filepath.
- Pushed code to Git.

**4/9/2023**
- Implemented tests to check and make sure that the logic is correct to manipulate pokemon values read from the CSV file correctly.
- Reviewed the application, checked comments, removed some unneeded code, and made sure the API was working as I complete the project. I had so much fun working on it!
- Pushed code to Git. 

### How to install and use

1. Create the database on your computer using the files in the sql folder within this project. Open the pokemon_db.sql file first in an IDE for databases (I used Microsoft SQL Server Management Studio 18), execute the file to create the database, then open the pokemon.sql file and execute to create pokemon table. This is where we will store our information.

2. In the appsettings.json file for the pokemon-api program, add the fully qualified filepath for the "pokemon.csv," then add the database connection string for your machine for "pokemon_db." The existing strings should be replaced.

3. In Microsoft Visual Studio, open the project solution and press run. The API should work! Pokemon information should be automatically entered into the database for you by the API with the stats modified in accordance with the specifications above and Ghost and Legendary Pokemon excluded. A browser window with the local host should come up. Users can test and interact with the API using browser windows and Postman; Postman will make it easy to see the status codes returned by the API. 

4. The single searchable and filterable endpoint with pagination should work with relevant query keys and values placed in any order following /pokemon. Note that in the current version of the API, filter values for HP, Attack, and Defense will return values that are less than or equal to what's put into the query. For example, the following "/pokemon?name=a&hp=50&attack=40&defense=70&page=1" will search for pokemon with "a" anywhere in the name, filter for pokemon with an hp stat of less than or equal to 50, with an attack stat of less than or equal to 40, a defense stat of less than or equal to 70, and the API will return results 1 through 10.

5. To test the application, open the test solution in Microsoft Visual Studio and run tests. The current set of tests check to make sure that the Pokemon class constructor manipulates the information received from the CSV file correctly as it creates new pokemon objects.

6. Check out my progress as I completed this project day by day on my personal GitHub: https://github.com/sjd58. The repo is titled "pokemon-api." It's currently pinned below my GitHub Readme so it should be easy to find.