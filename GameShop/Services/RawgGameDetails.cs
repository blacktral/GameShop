// RawgGameDetails.cs
using System.Text.Json.Serialization;

namespace GameShop.Services.Rawg.DTOs
{
    // Повна модель гри для сторінки деталей
    public class RawgGameDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("description_raw")]
        public string Description { get; set; } // Використовуйте 'description_raw' для чистого тексту

        [JsonPropertyName("released")]
        public DateOnly? Released { get; set; }

        [JsonPropertyName("background_image")]
        public string CoverImageUrl { get; set; }

        [JsonPropertyName("rating")]
        public decimal Rating { get; set; }

        [JsonPropertyName("developers")]
        public List<RawgCreator> Developers { get; set; }

        [JsonPropertyName("publishers")]
        public List<RawgCreator> Publishers { get; set; }

        [JsonPropertyName("clip")]
        public RawgClip Clip { get; set; }
    }

    // Допоміжні класи
    public class RawgClip
    {
        [JsonPropertyName("clip")]
        public string TrailerUrl { get; set; }
    }

    public class RawgCreator
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
