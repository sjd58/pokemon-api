using Microsoft.AspNetCore.Mvc;
using pokemon_api.DAOs;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace pokemon_api.Controllers
{
    // Controller will set up for the path everything before the word "controller" in the classname.
    // Therefore, the endpoint will begin with .../pokemon
    [Route("[controller]")]
    [ApiController]
    // Inherit from ControllerBase
    public class PokemonController : ControllerBase
    {
        // Add the interface to this class as a property
        private readonly IPokemonDao pokemonDao;

        // Dependency injection; set the interface property with a constructor
        public PokemonController(IPokemonDao _pokemonDao)
        {
            pokemonDao = _pokemonDao;
        }

        [HttpGet]
        // Setting default arguments; searching, filtering, and pagination will be triggered when these are different from their default values.
        public ActionResult<List<Pokemon>> SearchAndFilterPokemon(string name = "", int hp = 0, int attack = 0, int defense = 0, int page = 0)
        {
            List<Pokemon> listToReturn = new List<Pokemon>();
            listToReturn = pokemonDao.GetAndFilterPokemon(name, hp, attack, defense, page);
            // If we don't get a Pokemon from our search/filter, return 404, not found.
            if ((listToReturn.Count == 0) || (listToReturn == null))
            {
                return NotFound(listToReturn);
            }
            // Else, return 200 with the list of Pokemon
            else
            {
                return Ok(listToReturn);
            }
        }
    }
}
