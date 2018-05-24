using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaiderIo.Data.Model;
using RaiderIo.Services;

namespace RaiderIo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {
        private static RaiderIoService _raiderIo = new RaiderIoService();

        [Route("profile/example")]
        [HttpGet]
        public Profile GetExample() {
            var profile = _raiderIo.GetExample();
            return profile;
        }

        [Route("profile/{region}/{realm}/{character}")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Profile))]
        public IActionResult Get(string region, string realm, string character) {
            var profile = _raiderIo.Get(region, realm, character, "gear", "mythic_plus_scores", "mythic_plus_best_runs:all");
            return Ok(profile);
        }

        [Route("profile")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Profile))]
        public IActionResult Get([FromBody] CharacterLookup character) {
            var profile = _raiderIo.Get(character.region, character.realm, character.name, "gear", "mythic_plus_scores", "mythic_plus_best_runs:all");
            return Ok(profile);
        }

        [HttpPost, Route("profiles")]
        [ProducesResponseType(200, Type=typeof(IList<CharacterLookup>))]
        public IActionResult GetAll([FromBody] CharacterLookupGroup characters) {
            if (characters == null || characters.characters == null || !characters.characters.Any())
                return BadRequest();

            var profiles = _raiderIo.GetAll(characters.characters);
            return Ok(new{ characters = profiles});
        }

    }
}