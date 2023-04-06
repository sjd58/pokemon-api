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
- Break apart streamreader and DAO logic into separate classes
- Implement CLI to prompt users for filepaths and the SQL connection string
- Implement logic to add the information we want to the database, based on the description above.

### How to install and use

1. Create the database on your computer using the files in the sql folder within this project. Open the pokemon_db.sql file first in an IDE for databases (I used Microsoft SQL Server Management Studio 18), execute the file to create the database, then open the pokemon.sql file and execute to create pokemon table. This is where we will store our information.

2. We're going to read the pokemon.csv file in order to add the information we want to our database and filter out the information we don't, in accordance with the rules described above. To do this, we will need the fully qualified filepath for pokemon.csv. This can be found easily by locating the file in a file explorer, clicking on the file, then clicking on the path at the top of the window. Copy the filepath to the clipboard. Alternatively, locate the file in a terminal and use the path shown there. 

