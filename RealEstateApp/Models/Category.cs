using System.Text.Json.Serialization;

namespace RealEstateApp.Models
{
    public class Category (int id, string name, string imageUrl)
    {
        [JsonPropertyName("id")]
        public int Id { get; init; } = id;
        [JsonPropertyName("name")]
        public string Name { get; init; } = name;
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; init; } = imageUrl;
    }
}
