using System;
using System.Collections.Generic;
using System.Text;

namespace pokemon_filereader.Classes
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
        public int Speed { get; set;  }
        public int Generation { get; set; }
        public bool Legendary { get; set; }
        // custom constructor below
        /*
        public Pokemon (string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {

            }
        }
        */
    }
}
