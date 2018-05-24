using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RaiderIo.Data.Model;
using RestSharp;
using JsonSerializer = RestSharp.Serializers.JsonSerializer;

namespace RaiderIo.Services {
    public class RaiderIoService {

        private IRestClient _client = new RestClient("http://raider.io/api/v1/");

        private List<string> _validProfileFields = new List<string>() {
            "gear",
            "guild",
            "raid_progression",
            "mythic_plus_scores",
            "mythic_plus_ranks",
            "mythic_plus_recent_runs",
            "mythic_plus_best_runs",
            "mythic_plus_best_runs:all",
            "mythic_plus_highest_level_runs",
            "mythic_plus_weekly_highest_level_runs",
            "previous_mythic_plus_scores",
            "previous_mythic_plus_ranks",
            "raid_achievement_meta",
        };

        


        public RaiderIoService() {
            
        }

        public Profile GetExample() {
            return Get("us", "aegwynn", "kiarah", null);
        }

        
        public Profile Get(string region, string realm, string name, params string[] fields) {
            if (fields == null || fields.All(x => string.IsNullOrEmpty(x))) fields = _validProfileFields.ToArray();
            fields = fields.Intersect(_validProfileFields).ToArray();
            var request = new RestRequest("/characters/profile");
            request.AddParameter("region", region);
            request.AddParameter("realm", realm);
            request.AddParameter("name", name);
            request.AddParameter("fields", string.Join(",", fields));

            var response = _client.Execute(request);
            var content = response.Content;
            var result = JsonConvert.DeserializeObject<Profile>(content);
            return result;
        }

        public IList<Profile> GetAll(IList<CharacterLookup> characters, params string[] fields) {
            if (fields == null || fields.All(string.IsNullOrEmpty)) fields = _validProfileFields.ToArray();
            fields = fields.Intersect(_validProfileFields).ToArray();
            var profiles = new List<Profile>();
            foreach (var character in characters) {
                profiles.Add(Get(character.region, character.realm, character.name, fields));
            }
            return profiles;
        }
    }
}