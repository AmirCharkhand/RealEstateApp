
using System.Text.Json.Serialization;

namespace RealEstateApp.Models
{
    public class Property (int id, string name, string description, string address, string phone, decimal price, string imageUrl, bool isTrending = false)
    {
        [JsonPropertyName("id")]
        public int Id { get; init; } = id;
        [JsonPropertyName("name")]
        public string Name { get; init; } = name;
        [JsonPropertyName("detail")]
        public string Description { get; init; } = description;
        [JsonPropertyName("address")]
        public string Address { get; init; } = address;
        [JsonPropertyName("phone")]
        public string Phone { get; init; } = phone;
        [JsonPropertyName("price")]
        public decimal Price { get; init; } = price;
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; init; } = imageUrl;
        [JsonPropertyName("isTrending")]
        public bool IsTrending { get; set; } = isTrending;
    }
}
