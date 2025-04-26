using SQLite;

namespace RealEstateApp.Models
{
    public class BookmarkedProperty
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
