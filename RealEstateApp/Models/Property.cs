
namespace RealEstateApp.Models
{
    public class Property (int id, string name, string description, string address, string phone, decimal price, string imageUrl, bool isTrending = false)
    {
        public int Id { get; init; } = id;
        public string Name { get; init; } = name;
        public string Description { get; init; } = description;
        public string Address { get; init; } = address;
        public string Phone { get; init; } = phone;
        public decimal Price { get; init; } = price;
        public string ImageUrl { get; init; } = imageUrl;
        public bool IsTrending { get; set; } = isTrending;
    }
}
