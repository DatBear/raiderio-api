using System;
using System.Collections.Generic;
using System.Linq;
using LazyCache;
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
        private readonly IAppCache _cache;
        private readonly Func<DateTimeOffset> _cacheExpiration = () => DateTimeOffset.Now.AddMinutes(10);

        public CharactersController(IAppCache cache) {
            _cache = cache;
        }

        [Route("profile/example")]
        [HttpGet]
        public Profile GetExample() {
            Profile ExampleFactory() => _raiderIo.GetExample();
            var profile = _cache.GetOrAdd("exampleProfile", ExampleFactory);
            return profile;
        }

        [Route("profile/{region}/{realm}/{character}")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Profile))]
        public IActionResult GetProfile(string region, string realm, string character) {
            Profile ProfileFactory() => _raiderIo.Get(region, realm, character, "gear", "mythic_plus_scores", "mythic_plus_best_runs:all");
            var profile = _cache.GetOrAdd($"{region}/{realm}/{character}", ProfileFactory, _cacheExpiration());
            return Ok(profile);
        }

        [Route("profile")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Profile))]
        public IActionResult PostProfile([FromBody] CharacterLookup character) {
            Profile ProfileFactory() => _raiderIo.Get(character.region, character.realm, character.name, "gear", "mythic_plus_scores", "mythic_plus_best_runs:all");
            var profile = _cache.GetOrAdd($"{character.region}/{character.realm}/{character.name}", ProfileFactory, _cacheExpiration());
            return Ok(profile);
        }

        [HttpPost, Route("profiles")]
        [ProducesResponseType(200, Type=typeof(IList<CharacterLookup>))]
        public IActionResult GetAll([FromBody] CharacterLookupGroup characters) {
            if (characters == null || characters.characters == null || !characters.characters.Any())
                return BadRequest();
            var profiles = new List<Profile>();
            foreach (var character in characters.characters) {
                Profile ProfileFactory() => _raiderIo.Get(character.region, character.realm, character.name, "gear", "mythic_plus_scores", "mythic_plus_best_runs:all");
                var profile = _cache.GetOrAdd($"{character.region}/{character.realm}/{character.name}", ProfileFactory, _cacheExpiration());
                profiles.Add(profile);
            }
            return Ok(new { profiles });
        }

    }
}