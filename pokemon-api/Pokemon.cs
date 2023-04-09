using System;
using System.Collections.Generic;
using System.Text;

namespace pokemon_api
{
    public class Pokemon
    {
        public int PokemonNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Total { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public int Generation { get; set; }
        public bool Legendary { get; set; }

        // This class will have overloaded constructors.
        // This way, we can change how we implement the Pokemon properties depending on what we pass into the constructor.
        // For example, we will use the top constructor when retrieving Pokemon from the database when we do not want to modify any of the properties.
        // And we will use the bottom constructor when we add Pokemon to the database, modifying the properties in accordance with the project specifications.

        public Pokemon()
        {

        }

        // This constructor takes in the string array created by the streamreader and the split method.
        public Pokemon(string[] rawData)
        {
            // Parse the first string element and set the Pokemon Number property equal to the result
            PokemonNumber = int.Parse(rawData[0]);

            // Set Name equal to the second element
            Name = rawData[1];

            // Set Type1 equal to the third element
            Type1 = rawData[2];

            // Set Type2 equal to the fourth element
            Type2 = rawData[3];

            // Set Generation equal to the twelfth element.
            Generation = int.Parse(rawData[11]);

            // Parse the TRUE or FALSE to a bool value and set Legendary equal to it.
            // Although we don't want our database to include legendary pokemon right now, what if we wanted it to include them in the future? So we'll filter on the DAO.
            Legendary = bool.Parse(rawData[12]);

            // With this information, we now have everything we need to modify the Pokemon stats in accordance with the project specifications.
            // If Type1 or Type 2 is Steel, double the Pokemon's HP
            if (Type1 == "Steel" || Type2 == "Steel")
            {
                HP = int.Parse(rawData[5]) * 2;
            }
            // If neither type is Steel, just set the HP equal to what we receive from the rawData
            else
            {
                HP = int.Parse(rawData[5]);
            }

            // If Type1 or Type2 is Fire, we need to reduce the Pokemon's Attack by 10%.
            // We will do this by multiplying what we receive by 0.9, then casting the result back into an Int.
            if (Type1 == "Fire" || Type2 == "Fire")
            {
                Attack = (int)(int.Parse(rawData[6]) * 0.9);
            }
            // If neither type is Fire, just set attack to equal to what we receive from rawData
            else
            {
                Attack = int.Parse(rawData[6]);
            }

            // If a Pokemon has types Buy and Flying, we need to increase Speed by 10%.
            // I'm assuming that the order for these types does not matter (although I would confirm with the product owner)
            // therefore we will check for Bug and Flying, then Flying and Bug types.
            if (Type1 == "Bug" && Type2 == "Flying" || Type1 == "Flying" && Type2 == "Bug")
            {
                Speed = (int)(int.Parse(rawData[10]) * 1.1);
            }
            // If the Pokemon doesn't have types Bug or Flying, just set the Speed equal to what we get from the rawData.
            else
            {
                Speed = int.Parse(rawData[10]);
            }

            // If the Pokemon's name starts with the char 'G', we need to add 5 defense for each letter after the first in its name.
            // So, we need to determine if the Pokemon's name starts with 'G',
            // Then add five times the number of other letters other than 'G' in the Pokemon's name to the Pokemon's Defense stat.
            if (Name.StartsWith('G'))
            {
                int defenseMultiplier = Name.Length - 1; // Minus one because we're excluding 'G'
                Defense = int.Parse(rawData[7]) + 5 * defenseMultiplier;
            }
            // If the name doesn't start with 'G', just set Defense equal to what we get from the rawData
            else
            {
                Defense = int.Parse(rawData[7]);
            }

            // We didn't receive any special instructions for SpAttack or SpDefense, so just set those to what we find in the raw data.
            SpAttack = int.Parse(rawData[8]);
            SpDefense = int.Parse(rawData[9]);

            // Now that we have computed everything, we can now add up the stats to the total
            Total = HP + Attack + Defense + SpAttack + SpDefense + Speed;
        }
    }
}
