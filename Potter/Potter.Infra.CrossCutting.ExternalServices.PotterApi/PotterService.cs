using Potter.Domain.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Potter.Infra.CrossCutting.ExternalServices.PotterApi.Models;
using System.Collections.Generic;
using System.Linq;
using Polly;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using Polly.CircuitBreaker;

namespace Potter.Infra.CrossCutting.ExternalServices.PotterApi
{
    public class PotterService : IPotterService
    {
        private readonly string _potterUrl;
        private readonly string _potterKey;

        private readonly ILogger<PotterService> _logger;

        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

        public PotterService(IConfiguration configuration, ILogger<PotterService> logger)
        {
            _logger = logger;

            _potterUrl = configuration.GetSection("AppSettings")["potterUrl"];
            _potterKey = configuration.GetSection("AppSettings")["potterKey"];

            _circuitBreaker = Policy.Handle<Exception>()
                                       .CircuitBreakerAsync(
                                            exceptionsAllowedBeforeBreaking: 2,
                                            durationOfBreak: TimeSpan.FromSeconds(30),
                                            onBreak: (ex, timeSpan) =>
                                            {
                                                _logger.LogWarning($"\n\n------- Circuit Open -------\n\nError => {ex.Message}\n\n");
                                            },
                                            onReset: () =>
                                            {
                                                _logger.LogWarning("\n\n------- Circuit Closed -------\n\n");
                                            }
                                        );
        }

        public async Task<bool> ValidateHouse(string house)
        {
            _logger.LogInformation("Get Houses from Potter Api");

            List<PotterHouse> houses = new List<PotterHouse>();

            Func<CancellationToken, Task> cacheFunction = async (CancellationToken cancellationToken) => {
                houses = await GetHousesFromCache(cancellationToken);
            };

            var policy = Policy.Handle<Exception>()
                               .FallbackAsync(cacheFunction)
                               .WrapAsync(_circuitBreaker);

            await policy.ExecuteAsync(async () => {
                houses = await GetHouses();
            });

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

        private async Task<List<PotterHouse>> GetHousesFromCache(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Houses from Cache");

            return await Task.FromResult(new List<PotterHouse>()
            {
                new PotterHouse()
                {
                    Id = "5a05e2b252f721a3cf2ea33f",
                    Name = "Gryffindor",
                    Members = new List<string>()
                    {
                        "5a0fa648ae5bc100213c2332",
                        "5a0fa67dae5bc100213c2333",
                        "5a0fa7dcae5bc100213c2338",
                        "5a107e1ae0686c0021283b19",
                        "5a10944f3dc2080021cd8755",
                        "5a10947c3dc2080021cd8756"
                    }
                }
            });
        }
    }
}
