using Microsoft.AspNetCore.Mvc;
using pokemon_api.DAOs;
using pokemon_filereader.Classes;
using System.Collections.Generic;

namespace pokemon_api.Controllers
{
    // Set up the routing and make sure we are using the components we need
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
 
        [HttpGet("/pokemon")]
        // Based on the input, this needs to return a list of pokemon
        public ActionResult<List<Pokemon>> GetPokemon()
        {
            return Ok(pokemonDao.GetPokemon());
        }
    }
}
