namespace RealEstateApp.Models
{
    public class Category (int id, string name, string imageUrl)
    {
        public int Id { get; init; } = id;
        public string Name { get; init; } = name;
        public string ImageUrl { get; init; } = imageUrl;
    }
}
