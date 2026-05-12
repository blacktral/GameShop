// RawgSearchResponse.cs
using System.Text.Json.Serialization;

namespace GameShop.Services.Rawg.DTOs
{
    // Відповідь на пошук
    public class RawgSearchResponse
    {
        [JsonPropertyName("results")]
        public List<RawgGameSummary> Results { get; set; } = new List<RawgGameSummary>();
    }

    // Спрощена модель гри для результатів пошуку
    public class RawgGameSummary
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("released")]
        public DateOnly? Released { get; set; }

        [JsonPropertyName("background_image")]
        public string CoverImageUrl { get; set; }
    }
}