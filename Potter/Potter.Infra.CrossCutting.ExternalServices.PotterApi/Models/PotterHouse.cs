using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Potter.Infra.CrossCutting.ExternalServices.PotterApi.Models
{
    public class PotterHouse
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Members { get; set; }
    }
}
