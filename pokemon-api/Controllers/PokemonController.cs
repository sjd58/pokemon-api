using Microsoft.AspNetCore.Mvc;
using pokemon_api.DAOs;
using pokemon_filereader.Classes;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace pokemon_api.Controllers
{
    // Controller will set up for the path everything before the word "controller" in the classname
    [Route("[controller]")]
    [ApiController]
    // Inherit from ControllerBase
    public class PokemonController : ControllerBase
    {
        // Add the interface to this class as a property
        private readonly IPokemonDao pokemonDao;

        // Set the interface property with a constructor
        public PokemonController(IPokemonDao _pokemonDao)
        {
            pokemonDao = _pokemonDao;
        }
 
        //[HttpGet()]
        // Based on the input, this needs to return a list of pokemon
        public ActionResult<List<Pokemon>> GetPokemon()
        {
            return Ok(pokemonDao.GetPokemon());
        }

        [HttpGet("{name}")]
        public ActionResult<List<Pokemon>> GetPokemonByName(string name)
        {
            return Ok(pokemonDao.GetPokemonByName(name));
        }
        /*
        [HttpGet("filter")]
        public ActionResult<List<Pokemon>> GetPokemonByPage(int page)
        {
            return Ok(pokemonDao.GetPokemonByPage(page));
        }
        */

        [HttpGet()]
        public ActionResult<string> ReturnFilterString(string name = "", int hp = 0, int attack = 0, int defense = 0, int page = 0)
        {
            return Ok(pokemonDao.GetAndFilterPokemon(name, hp, attack, defense, page));
        }
        

        // check to see if what we're getting is not zero; if it's not zero, we took something into this method and we can work with it.

        // alternatively, you could try taking in all of these as strings and you can check if they're null then.
    }
}
