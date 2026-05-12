using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json; // Потрібен цей using!
using System.Collections.Generic;
using GameShop.Services.Rawg.DTOs;
using Microsoft.Extensions.Configuration;

namespace GameShop.Services
{
    public interface IRawgApiService
    {
        Task<RawgSearchResponse?> SearchGamesAsync(string query);
        Task<RawgGameDetails?> GetGameDetailsAsync(int id);
    }

    public class RawgApiService : IRawgApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;


        public RawgApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            _apiKey = config["RawgApiKey"] ?? "ce1eb3878981422abe16188d19c114e7";
            _httpClient.BaseAddress = new Uri("https://api.rawg.io/api/");
        }

        public async Task<RawgSearchResponse?> SearchGamesAsync(string query)
        {
            var url = $"games?key={_apiKey}&search={query}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();


            return await response.Content.ReadFromJsonAsync<RawgSearchResponse>();
        }

        public async Task<RawgGameDetails?> GetGameDetailsAsync(int id)
        {
            var url = $"games/{id}?key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RawgGameDetails>();
        }
    }
}

