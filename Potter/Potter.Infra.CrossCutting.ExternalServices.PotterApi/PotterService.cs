using Potter.Domain.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Potter.Infra.CrossCutting.ExternalServices.PotterApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Potter.Infra.CrossCutting.ExternalServices.PotterApi
{
    public class PotterService : IPotterService
    {
        private readonly string _potterUrl;
        private readonly string _potterKey;

        public PotterService(string potterUrl, string potterKey)
        {
            _potterUrl = potterUrl;
            _potterKey = potterKey;
        }

        public async Task<bool> ValidateHouse(string house)
        {
            List<PotterHouse> houses = await GetHouses();
            return houses.Any(x => x.Id.Equals(house));
        }

        private async Task<List<PotterHouse>> GetHouses()
        {
            var requestUri = $"{_potterUrl}houses?key={_potterKey}";

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<PotterHouse>>(responseBody);
            }
        }
    }
}
